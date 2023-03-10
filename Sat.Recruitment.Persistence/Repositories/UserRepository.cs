using Mapster;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Core.ResponsesExceptions;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.IRepositories;
using Sat.Recruitment.Persistence.Models;
using Entities = Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SatRecruitmentDbContext _context;

        public UserRepository(SatRecruitmentDbContext context)
        {
            _context = context;
        }
        public async Task<Entities.User> Insert(Entities.User user)
        {
            var existingUser = await GetUserByUniqueFields(user.Email, user.Phone, user.Name, user.Address);

            if (existingUser != null)
            {
                throw new BadRequestException()
                {
                    ErrorMessage = SatRecruitmentConstants.ErrorMsgUserDuplicate
                };
            }

            User sqlUser = user.Adapt<User>();

            var sqlResult = _context.Users.Add(sqlUser);

            await _context.SaveChangesAsync();

            return sqlResult.Entity.Adapt<Entities.User>();
        }

        public async Task<Entities.User?> GetUserByUniqueFields(string email, string phone, string name, string address)
        {
            var sqlResult = await _context.Users.FirstOrDefaultAsync(c => c.Email == email || c.Phone == phone || c.Name == name || c.Address == address);

            return sqlResult?.Adapt<Entities.User>();

        }

        public async Task<bool> ValidateCredentials(string email, string password)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            if (result != null)
            {
                return true;
            }
            return false;
        }

        public async Task<string> GetUserTypeByEmail(string email)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (result == null)
            { 
                throw new NotFoundException()
                {
                    ErrorMessage = SatRecruitmentConstants.ErrorMsgNotFound
                };
            }
            return result.UserType.Trim();
        }

    }
}
