using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Api.Contexts;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public UserRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserModelDto> CreateUpdate(UserModelDto userDto)
        {
            UserModel user = _mapper.Map<UserModelDto, UserModel>(userDto);

            if (user.UserId != Guid.Empty && user.UserId != null)
            {
                _db.Users.Update(user);
            }
            else
            {
                _db.Users.Add(user);
            }

            await _db.SaveChangesAsync();

            return _mapper.Map<UserModel, UserModelDto>(user);
        }

        public async Task<bool> Delete(Guid userId)
        {
            try
            {
                UserModel user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (ReferenceEquals(user, null))
                {
                    return false;
                }
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<UserModelDto> GetById(Guid userId)
        {
            UserModel user = await _db.Users.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            return _mapper.Map<UserModelDto>(user);
        }

        public async Task<IEnumerable<UserModelDto>> Get()
        {
            List<UserModel> userList = await _db.Users
                .ToListAsync();
            return _mapper.Map<List<UserModelDto>>(userList);
        }

        public async Task<bool> FindDuplicate(UserModelDto userDto)
        {
            bool isRegister = false;

            var search = await _db.Users.Where(
                    x => x.Email == userDto.Email || x.Phone == userDto.Phone || (x.Name == userDto.Name && x.Address == userDto.Address)
            ).FirstOrDefaultAsync();


            if (search != null)
            {
                isRegister = true;
            }

            return isRegister;
        }
    }
}
