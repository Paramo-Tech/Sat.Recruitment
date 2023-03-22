using System;
using Sat.Recruitment.Domain.Entities.UserAgregate;
using Sat.Recruitment.Domain.Entities.UserAgregate.Rules;

namespace Sat.Recruitment.Domain.Entities.UserAggregate
{
	public class User
	{
        public int Id { get; set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public string UserType { get; private set; }
        public decimal Money { get; private set; }

        public User(string name, string email, string address, string phone, string userType, decimal money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = userType;
            Money = money + CalculateGif.CreateGif(userType).CalculateGif(money);
        }
    }
}

