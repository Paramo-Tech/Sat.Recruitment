using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Core.Abstractions.Repositories;
using Sat.Recruitment.Core.DomainEntities;
using Sat.Recruitment.Infrastructure.EF.EFSpecifics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RecruitmentDbContext _dbContext;

        public UserRepository(RecruitmentDbContext recruitmentDbContext)
        {
            this._dbContext = recruitmentDbContext;
        }

        public async Task<List<User>> GetAll(Func<User, bool> filter = null)
        {
            if (filter == null)
            {
                return await _dbContext.Users.ToListAsync();
            }
            else
            {
                return _dbContext.Users.Where(filter).ToList();
            }
        }

        /// <summary>
        /// Returns the User corresponding to the Id received by parameter,
        /// or null if the user does not exist.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<User> GetById(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Create Id for the new user
            user.Id = Guid.NewGuid();

            // Add user to Db
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task Delete(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            User user = await _dbContext.Users.FirstOrDefaultAsync(u => id == u.Id);

            // Remove User from Db
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<User> Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            User persistedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            // Remove User from Db
            if (persistedUser != null)
            {
                // Update the fields of the persisted entity, with the new fields
                // received by parameter
                persistedUser.Name = user.Name;
                persistedUser.Email = user.Email;
                persistedUser.Address = user.Address;
                persistedUser.Phone = user.Phone;
                persistedUser.UserType = user.UserType;
                persistedUser.Money = user.Money;

                // Update User in Db
                _dbContext.Users.Update(persistedUser);
                await _dbContext.SaveChangesAsync();
            }

            return persistedUser;
        }
    }
}
