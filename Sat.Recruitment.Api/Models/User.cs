using Sat.Recruitment.Api.Enums;
using Sat.Recruitment.Api.Strategies;
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

        }

        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email
        {
            get { return _email; }
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
            get { return _money;  }
            set { _money = value + _moneyStrategy.CalculateAdditionalMoney(value); ; }
        }
    }
}
