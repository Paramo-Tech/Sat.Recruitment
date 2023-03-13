using System;
using Sat.Recruitment.Domain.ValueObjects;

namespace Sat.Recruitment.Domain.Aggregates
{
	public class User : AggregateRoot
	{
        public UserName Name { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public Phone Phone { get; private set; }
        public UserType Type { get; private set; }
        public Money Money { get; private set; }
        public User(UserName name, Email email, Address address, Phone phone, UserType type, Money value)
		{
            this.Address = address;
            this.Email = email;
            this.Money = value;
            this.Name = name;
            this.Type = type;
            this.Phone = phone;
		}

        public static User Create(UserName name, Email email, Address address, Phone phone, UserType type, Money value)
        {
            var user = new User(name, email, address, phone, type, value);
            return user;
        }
	}
}

