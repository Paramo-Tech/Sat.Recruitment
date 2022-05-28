using System;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Contracts;

namespace Sat.Recruitment.Api.Domain.Services
{
    public abstract class UserBaseBuilder
    {
        protected readonly IUserModel ModelUserModel;

        protected UserBaseBuilder(IUserModel modelUserModel)
        {
            ModelUserModel = modelUserModel;
        }

        private static string NormalizeEmail(string email)
        {
            var aux = email.Split(new char[] {'@'}, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", aux[0], aux[1]);
        }

        public User Build()
        {
            return new User
            {
                Name = ModelUserModel.Name,
                Email = NormalizeEmail(ModelUserModel.Email),
                Address = ModelUserModel.Address,
                Phone = ModelUserModel.Phone,
                UserType = ModelUserModel.UserType,
                Money = CalculeMoney()
            };
        }

        public abstract decimal CalculeMoney();
    }
}