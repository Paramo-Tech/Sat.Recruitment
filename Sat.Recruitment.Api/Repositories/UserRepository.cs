using MongoDB.Bson;
using MongoDB.Driver;
using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repositories
{
    [ExcludeFromCodeCoverage]
    public class UserRepository : IUserRepository
    {
        private IMongoCollection<User> _users;
        [ExcludeFromCodeCoverage]
        public UserRepository(IDBSettings settings)
        {
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            _users = database.GetCollection<User>(settings.Collection);
            var options = new CreateIndexOptions { Unique = true };
            var keys = Builders<User>.IndexKeys
                .Ascending(u => u.Email)
                .Ascending(u => u.Phone)
                .Ascending(u => u.Name)
                .Ascending(u => u.Address);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var model = new CreateIndexModel<User>(keys, indexOptions);
            _users.Indexes.CreateOne(model);
        }
        public void CreateUser(User user)
        {
            _users.InsertOne(user);
        }

        public async Task<IEnumerable<User>> GetAllUsers() 
        {
            return await _users.FindAsync(user => true).Result.ToListAsync();
        }

        public async Task<bool> IsUserDuplicated(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, user.Email) 
                | Builders<User>.Filter.Eq(u => u.Phone, user.Phone) 
                | Builders<User>.Filter.Eq(u => u.Name, user.Name) 
                & Builders<User>.Filter.Eq(u => u.Address, user.Address);
                
            var u = await _users.FindAsync(filter).Result.FirstOrDefaultAsync();

            return u != null;
        }
    }
}
