using System;
using Sat.Recruitment.Domain.Aggregates;
using Sat.Recruitment.Domain.ValueObjects;

namespace Sat.Recruitment.Domain
{
	public interface IUserRepository
	{
		public Task Save(User user);
		public Task<User> SearchBy(Email email, Phone phone);
        public Task<User> SearchBy(UserName name, Address address);
    }
}

