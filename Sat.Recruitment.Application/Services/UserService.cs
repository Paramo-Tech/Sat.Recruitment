using Sat.Recruitment.Application.Common.Interfaces.Persistance;
using Sat.Recruitment.Application.Services.Interfaces;
using Sat.Recruitment.Contracts.Results;
using Sat.Recruitment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        protected readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<Result> AddItemAsync(User user)
        {
            var validationData = ValidateUserData(user);

            if (validationData != null && !validationData.Result.IsSuccess)
                return validationData.Result;

            user.Money = GetTypeUserMoneyGif(user.UserType, user.Money);
            user.Email = GetNormalizeEmail(user.Email);

            var validationRepo = ValidateUserRepo(user);

            if (validationRepo != null && !validationRepo.Result.IsSuccess)
                return validationRepo.Result;

            return await _userRepository.AddAsync(user);
        }

        public override async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<Result> ValidateUserData(User user)
        {
            Result result = new Result() { IsSuccess = true, Errors = string.Empty };
            string errors = "";
            if (String.IsNullOrEmpty(user.Name))
                //Validate if Name is null
                errors = "The name is required";
            if (String.IsNullOrEmpty(user.Email))
                //Validate if Email is null
                errors += (String.IsNullOrEmpty(errors) ? "" : "; ") + "The email is required";
            if (String.IsNullOrEmpty(user.Address))
                //Validate if Address is null
                errors += (String.IsNullOrEmpty(errors) ? "" : "; ") + "The address is required";
            if (String.IsNullOrEmpty(user.Phone))
                //Validate if Phone is null
                errors += (String.IsNullOrEmpty(errors) ? "" : "; ") + "The phone is required";

            if (!string.IsNullOrEmpty(errors)) result = new Result() { IsSuccess = false, Errors = errors };

            return await Task.FromResult(new Result() { IsSuccess = result.IsSuccess, Errors = result.Errors });
        }

        public async Task<Result> ValidateUserRepo(User user)
        {
            Result result = new Result() { IsSuccess = true, Errors = string.Empty };

            var usersList = await _userRepository.GetAllAsync();

            foreach (var item in usersList) item.Email = GetNormalizeEmail(item.Email);

            if (usersList.Count > 0 &&
                usersList
                .Where(x => x.Name == user.Name || x.Email == user.Email || x.Phone == user.Phone || x.Address == user.Address)
                .Count() > 0
                )
                result = new Result() { IsSuccess = false, Errors = "User is duplicated" };

            return await Task.FromResult(new Result() { IsSuccess = result.IsSuccess, Errors = result.Errors });
        }

        public decimal GetTypeUserMoneyGif(string userType, decimal money)
        {
            decimal percentage = userType switch
            {
                "Normal" => money > 100 ? Convert.ToDecimal(0.12) : money > 10 ? Convert.ToDecimal(0.08) : 0,
                "SuperUser" => money > 100 ? Convert.ToDecimal(0.20) : 0,
                "Premium" => money > 100 ? 2 : 0,
                _ => 0,
            };
            decimal moneyGif = money + (money * percentage);
            return moneyGif;
        }

        public string GetNormalizeEmail(string email)
        {
            //Normalize email
            if (email.Contains('@'))
            {
                var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

                var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

                aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

                return string.Join("@", new string[] { aux[0], aux[1] });
            }
            else
            {
                return email;
            }

        }
    }
}
