using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Controllers.Entity;
using Sat.Recruitment.Api.Logic.Interface;
using System;

namespace Sat.Recruitment.Api.Logic
{
    public class User
    {
        Ihelpers h = new Helpers();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }

        public void setValues(RequestUser user)
        {
            this.Name = user.Name;
            this.Email = h.normalizeEmail(user.Email);
            this.Address = user.Address;
            this.Phone = user.Phone;
            this.UserType = user.UserType;
            this.Money = setMoney(user.Money);
        }

        public virtual decimal setMoney(string money)
        {
            try
            {
                decimal percentage = 0;
                decimal dmoney = decimal.Parse(money);
                if (dmoney > 100)
                {
                    percentage = Convert.ToDecimal(0.12);
                }
                else if (dmoney <= 100 && dmoney > 10)
                {
                    percentage = Convert.ToDecimal(0.8);
                }
                else 
                {
                    throw new InvalidOperationException("Invalid money");
                }
                return h.convertMoney(dmoney, percentage);
            }
            catch
            {
                throw new InvalidOperationException("Invalid money");
            }

        }

    }
}
