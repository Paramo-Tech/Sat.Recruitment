using System;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Aggregates;
using Sat.Recruitment.Domain.ValueObjects;

namespace Sat.Recruitment.Application
{
	public class UserCreator
	{
        private readonly IUserRepository repository;
		public UserCreator(IUserRepository repository)
		{
            this.repository = repository;
		}

        public async Task Execute (UserRequest request)
        {
            var user = User.Create(new UserName(request.Name), new Email(request.Email), new Address(request.Address), new Phone(request.Phone), new UserType(request.UserType), new Money(request.Money));
            // Validate duplicate user
            await this.repository.Save(user);
        }
    }
}

