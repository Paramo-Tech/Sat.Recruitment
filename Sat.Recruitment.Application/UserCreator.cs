using System;
using System.Diagnostics;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Aggregates;
using Sat.Recruitment.Domain.Exceptions;
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
            var existingUser = await repository.SearchBy(u => (u.Email.Value == request.Email || u.Phone.Value == request.Phone) || (u.Name.Value == request.Name && u.Address.Value == request.Address));
            if (existingUser != null)
            {
                Debug.WriteLine("The user is duplicated");
                throw new DuplicateUserException(existingUser.Name, existingUser.Email);
            }
            var user = User.Create(new UserName(request.Name), new Email(request.Email), new Address(request.Address), new Phone(request.Phone), new UserType(request.UserType), new Money(request.Money));
            await this.repository.Save(user);
        }
    }
}

