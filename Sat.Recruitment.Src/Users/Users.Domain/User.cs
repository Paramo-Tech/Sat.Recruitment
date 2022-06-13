using Shared.Domain;
using Shared.Domain.Exceptions;

namespace Users.Domain
{
    public class User
    {
        public string Name { get; set; }
        public Email Email { get; set; }
        public string Address { get; set; }
        public Phone Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }

        public User(
            string name,
            Email email,
            string address,
            Phone phone,
            UserType userType,
            decimal money)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException("The name can't be empty");
            }

            if (string.IsNullOrEmpty(address))
            {
                throw new DomainException("The address can't be empty");
            }

            ValidateMoney(money);

            this.Name = name;
            this.Email = email;
            this.Phone = phone;
            this.Address = address;
            this.UserType = userType;
            this.Money = money;
        }

        public void AddMoney(decimal money)
        {
            ValidateMoney(money); 
            this.Money += money;
        }

        private static void ValidateMoney(decimal money)
        {
            if (money < decimal.Zero)
            {
                throw new DomainException("Money can't be less than zero");
            }
        }
    }
}
