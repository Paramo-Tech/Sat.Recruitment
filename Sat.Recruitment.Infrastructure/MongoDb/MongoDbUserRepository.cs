using System;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using Amazon.Runtime.Internal;
using MongoDB.Driver;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Aggregates;
using Sat.Recruitment.Domain.ValueObjects;

namespace Sat.Recruitment.Infrastructure.MongoDb
{
	public class MongoDbUserRepository : IUserRepository
    {
        private readonly IMongoDatabase db;

		public MongoDbUserRepository(IMongoDatabase db)
		{
            this.db = db;
		}

        public async Task Save(User user)
        {
            var users = db.GetCollection<UserDocument>("users");
            await users.InsertOneAsync(new UserDocument() { Address= user.Address.Value, Email=user.Email.Value, Money=user.Money.Value, Name= user.Name.Value, Phone= user.Phone.Value, UserType= user.Type.Value });
            //throw new NotImplementedException();
        }

        public Task<User?> SearchBy(Func<User, bool> predicate)
        {
            var users = db.GetCollection<UserDocument>("users");
            //Builders<UserDocument>.Filter.Where(d=>GetFilterExpression<UserDocument>(d));
            throw new NotImplementedException();
        }

        public async Task<User?> SearchBy(Email email, Phone phone)
        {
            var users = db.GetCollection<UserDocument>("users");
            var result = await users.FindAsync(u => u.Email == email.Value || u.Phone == phone.Value);
            var user = await result.FirstOrDefaultAsync();
            return user!=null?new User(new UserName(user.Name), new Email(user.Email), new Address(user.Address), new Phone(user.Phone), new UserType(user.UserType), new Money(user.Money)): null;
        }
        public async Task<User?> SearchBy(UserName name, Address address)
        {
            var users = db.GetCollection<UserDocument>("users");
            var result = await users.FindAsync(u => u.Name == name.Value && u.Address ==address.Value);
            var user = await result.FirstOrDefaultAsync();
            return user != null ? new User(new UserName(user.Name), new Email(user.Email), new Address(user.Address), new Phone(user.Phone), new UserType(user.UserType), new Money(user.Money)) : null;
        }
    }
}

