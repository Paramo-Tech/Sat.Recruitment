using Sat.Recruitment.Business.Base;
using Sat.Recruitment.Business.Contracts;
using Sat.Recruitment.DAccess;
using Sat.Recruitment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sat.Recruitment.Business
{
    public class UserBusiness : BaseBusiness<User>, IUserBusiness
    {
        private ICalculateMoneyUserBusiness calculoMoney = new CalculateMoneyNormalUser();

        public UserBusiness()
        {
            this.DataAccess = new UserDAccess();
            
        }

        private void SetCalculateMoneyMethod(ICalculateMoneyUserBusiness calculoMoney)
        {
            this.calculoMoney = calculoMoney;

        }

        private void Calculate(User user)
        {
            calculoMoney.CalculateMoney(user);

        }

        public async Task<Result> CreateUser(User item)
        {
            try
            {
                ConfigureItem(item);
                return await this.Create(item);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void ConfigureItem(User item)
        {
            try
            {
                if (item.UserType == UserType.SuperUser.ToString())
                    this.SetCalculateMoneyMethod(new CalculateMoneySuperUser());
                if (item.UserType == UserType.Premium.ToString())
                    this.SetCalculateMoneyMethod(new CalculateMoneyPremiumUser());
                if (item.UserType == UserType.Normal.ToString())
                    this.SetCalculateMoneyMethod(new CalculateMoneyNormalUser());
                this.Calculate(item);
                this.NormalizeMail(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void NormalizeMail(User item)
        {
            var aux = item.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            item.Email = string.Join("@", new string[] { aux[0], aux[1] });

        }

        public override bool Validate(User item, ref string errors)
        {
            errors = string.Empty;

            if (string.IsNullOrEmpty(item.Name))
                errors = " | The name is required";
            if (string.IsNullOrEmpty(item.Email))
                errors += " | The email is required";
            if (string.IsNullOrEmpty(item.Address))
                errors += " | The address is required";
            if (string.IsNullOrEmpty(item.Phone))
                errors += " | The phone is required";

            if (!string.IsNullOrEmpty(errors))
                errors = errors[3..]; // removes first 3 chars " | "

            return string.IsNullOrEmpty(errors);

        }

        public override Func<User, bool> Filter(User item)
        {
            return user => user.Email == item.Email || user.Phone == item.Phone
                                            || (user.Name == item.Name && user.Address == item.Address);
        }

        

    }

}
