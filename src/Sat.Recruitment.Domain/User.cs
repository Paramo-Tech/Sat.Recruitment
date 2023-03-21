using System;
namespace Sat.Recruitment.Domain
{
	public class User
	{
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
            Money = money;
        }
    }
}

