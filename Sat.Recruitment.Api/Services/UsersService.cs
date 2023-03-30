using Microsoft.OpenApi.Models;
using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public class UsersService : ServiceBase<User>, IUsersService
    {
        protected readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository) : base(usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<Result> Add(User user)
        {
            var validationData = ValidateUserData(user);

            if (validationData != null && !validationData.Result.IsSuccess)
                return validationData.Result;

            user.Money = GetTypeUserMoneyGif(user.UserType, user.Money);
            user.Email = GetNormalizeEmail(user.Email);

            var validationRepo = ValidateUserRepo(user);

            if (validationRepo != null && !validationRepo.Result.IsSuccess)
                return validationRepo.Result;

            return await _usersRepository.AddAsync(user);
        }

        public async Task<List<User>> GetAll()
        {
            return await _usersRepository.GetAllAsync();
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
            
            return await Task.FromResult(new Result () { IsSuccess = result.IsSuccess, Errors = result.Errors });
        }

        public async Task<Result> ValidateUserRepo(User user)
        {
            Result result = new Result() { IsSuccess = true, Errors = string.Empty };

            var usersList = await _usersRepository.GetAllAsync();

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
