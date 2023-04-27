using System;
using Sat.Recruitment.Domain.Common;
using Sat.Recruitment.Domain.Enums;

namespace Sat.Recruitment.Domain.Entities
{
    public class User : BaseAuditableEntity<Guid>
    {
        public User(string name, string email, string address, string phone, UserTypes userType, decimal money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = userType;
            Money = money;
        }

        public User(string name, string email, string address, string phone, UserTypes userType, decimal money, bool isNewUser)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = userType;
            Money = money;

            if (isNewUser)
                ApplyPromotion();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserTypes UserType { get; set; }
        public decimal Money { get; set; }

        private void ApplyPromotion()
        {
            decimal modifiedMoney = Money;
            switch (UserType)
            {
                case UserTypes.Normal:
                    {
                        if (modifiedMoney > 100M)
                            modifiedMoney += Money * 0.12M;
                        else if (modifiedMoney < 100M && modifiedMoney > 10M)
                            modifiedMoney += Money * 0.8M;
                        break;
                    }
                case UserTypes.SuperUser:
                    {
                        if (modifiedMoney > 100M)
                            modifiedMoney += Money * 0.2M;
                        break;
                    }
                case UserTypes.Premium:
                    {
                        if (modifiedMoney > 100M)
                            modifiedMoney += Money * 2M;
                        break;
                    }

            }

            Money = modifiedMoney;
        }
    }
}
