using MongoDB.Bson;
using MongoDB.Driver;
using Sat.Recruitment.Core.Exceptions;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Models.User;

namespace Sat.Recruitment.Services.Services
{
    /// <summary>
    /// Implementation of IUserService to Mongodb
    /// </summary>
    public class MongoUserService : IUserService
    {
        private readonly IMongoCollection<User> _userCollection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mongoDBSettings">Mongo driver settings</param>
        public MongoUserService(IMongoDbSettings mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.DatabaseName);
            _userCollection = database.GetCollection<User>(mongoDBSettings.CollectionName);
        }

        /// <summary>
        /// Add user async
        /// </summary>
        /// <param name="user">New user to add</param>
        /// <returns>void</returns>
        public async Task AddAsync(IUser user)
        {
            await _userCollection.InsertOneAsync((User)user);
        }

        /// <summary>
        /// Delete user async
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>void</returns>
        public async Task DeleteAsync(object id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
            await _userCollection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Get all users async
        /// </summary>
        /// <returns>List of users</returns>
        public async Task<IEnumerable<IUser>> GetAsync()
        {
            return await _userCollection.Find(new BsonDocument()).ToListAsync();
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User</returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<IUser?> GetAsyncById(object id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
            var users = await _userCollection.Find(filter).ToListAsync();
            if (users is null)
                throw new NotFoundException();
            return users.FirstOrDefault(x => x is not null);
        }
    }
}
