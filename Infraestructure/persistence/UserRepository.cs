using Application.Interfaces;
using Application.InterfacesApplication;
using Domain.Entities;
using Domain.Events;
using Infraestructure.Configdb;
using Infraestructure.dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.persistence
{
    public class UserRepository : IUserRepository
    {
        private ApiDbContext _apiDbContext;

        public UserRepository(ApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }
        public async Task<bool> Create(IUserType user)
        {
            var userDto = new UserDto()
            { Name = user.Name, Email = user.Email, Address = user.Address, Phone = user.Phone, UserType = user.UserType.ToString(), Money = user.Money };

            _apiDbContext.userDto.Add(userDto);
            var insert = await _apiDbContext.SaveChangesAsync();
            if (insert != 0)
            {
                return true;
            }

            return false;
        }

        public async Task<IUserType> GetUserById(int id)
        {
            UserDto user = await _apiDbContext.userDto
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync().ConfigureAwait(true);

            if (user == null)
                throw new KeyNotFoundException("user not found");

            return ConvertDtotoDomain(user);

        }

        public async Task<IUserType> GetUserByName(string name, string email)
        {
            UserDto user = await _apiDbContext.userDto
                .AsNoTracking()
                .Where(x => x.Name == name && x.Email == email)
                .FirstOrDefaultAsync().ConfigureAwait(true);

            if (user == null)
                return null;

            return ConvertDtotoDomain(user);

        }


        public bool Update(UserDomain user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IUserType ConvertDtotoDomain(UserDto userDto)
        {
            switch (userDto.UserType)
            {
                case "Normal":
                    return new UserDomain() { Name = userDto.Name, Email = userDto.Email, Address = userDto.Address, Phone = userDto.Phone, UserType = (Domain.Enums.UserType)Enum.Parse<UserType>(userDto.UserType), Money = userDto.Money };

                case "SuperUser":
                    return new SuperDomain() { Name = userDto.Name, Email = userDto.Email, Address = userDto.Address, Phone = userDto.Phone, UserType = (Domain.Enums.UserType)Enum.Parse<UserType>(userDto.UserType), Money = userDto.Money };
                case "Premium":
                    return new PremiumDomain() { Name = userDto.Name, Email = userDto.Email, Address = userDto.Address, Phone = userDto.Phone, UserType = (Domain.Enums.UserType)Enum.Parse<UserType>(userDto.UserType), Money = userDto.Money };
                default:
                    break;
            }
            return new UserDomain() { Name = userDto.Name, Email = userDto.Email, Address = userDto.Address, Phone = userDto.Phone, UserType = (Domain.Enums.UserType)Enum.Parse<UserType>(userDto.UserType), Money = userDto.Money };
        }

    }
}
