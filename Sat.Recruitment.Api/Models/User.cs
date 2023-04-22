using Sat.Recruitment.Api.Enums;
using Sat.Recruitment.Api.Strategies;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Models
{
    public class User
    {
        private IUserMoneyStrategy _moneyStrategy;

        private decimal _money;

        private string _email;

        public User()
        {


        }

        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email
        {
            get
            {
                var aux = _email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

                var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

                aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

                return string.Join("@", new string[] { aux[0], aux[1] });
            }
            set { _email = value; }
        }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [EnumDataType(typeof(UserType), ErrorMessage = "mensaje")]
        public UserType UserType { get; set; }
        [Required]
        public decimal Money
        {
            get
            {
                switch (this.UserType)
                {
                    case UserType.Normal:
                        _moneyStrategy = new NormalUserMoneyStrategy();
                        break;
                    case UserType.SuperUser:
                        _moneyStrategy = new SuperUserMoneyStrategy();
                        break;
                    case UserType.Premium:
                        _moneyStrategy = new PremiumUserMoneyStrategy();
                        break;
                    default:
                        throw new System.Exception("Invalid User Type");
                }
                return _money + _moneyStrategy.CalculateAdditionalMoney(_money);
            }
            set { _money = value; }
        }
    }
}
