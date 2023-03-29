using Microsoft.OpenApi.Models;
using Sat.Recruitment.Api.Interfaces;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
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

        public Task<Result> Add(User user)
        {
            user.Money = GetTypeUserMoneyGif(user.UserType, user.Money);
            user.Email = NormalizeEmail(user.Email);
            Result result = ValidateUser(user);
            //if (!result.IsSuccess);
            
            throw new System.NotImplementedException();
        }

        public async Task<List<User>> GetAll()
        {
            return await _usersRepository.GetAllAsync();
        }

        public Result ValidateUser(User user)
        {
            Result result = new Result() { IsSuccess = true, Errors = string.Empty };
            string errors = "";
            if (String.IsNullOrEmpty(user.Name))
                //Validate if Name is null
                errors = "The name is required";
            if (String.IsNullOrEmpty(user.Email))
                //Validate if Email is null
                errors = errors + " The email is required";
            if (String.IsNullOrEmpty(user.Address))
                //Validate if Address is null
                errors = errors + " The address is required";
            if (String.IsNullOrEmpty(user.Phone))
                //Validate if Phone is null
                errors = errors + " The phone is required";

            if (!string.IsNullOrEmpty(errors)) result = new Result() { IsSuccess = false, Errors = errors };
            
            return result;
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
            decimal moneyGif = money + money * percentage;
            return moneyGif;
        }

        public string NormalizeEmail(string email)
        {
            //Normalize email
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
