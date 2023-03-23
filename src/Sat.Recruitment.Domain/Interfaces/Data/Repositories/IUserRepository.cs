using System;
using Sat.Recruitment.Domain.Entities.UserAggregate;

namespace Sat.Recruitment.Domain.Interfaces.Data.Repositories
{
	public interface IUserRepository
	{
		Task<int> AddUserAsync(User user);
		Task<User> GetUserByIdAsync(int userId);
		Task<bool> IsDublicateUser(string name, string email, string address, string phone);
	}
}

