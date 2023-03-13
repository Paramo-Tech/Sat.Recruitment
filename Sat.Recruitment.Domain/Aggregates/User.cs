using System;
using Sat.Recruitment.Domain.ValueObjects;

namespace Sat.Recruitment.Domain.Aggregates
{
	public class User : AggregateRoot
	{
        public UserName Name { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public UserType Type { get; private set; }
        public Money Money { get; private set; }
        public User()
		{
		}
	}
}

