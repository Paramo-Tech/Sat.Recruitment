using Application.Handlers;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        public UserService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }
        public async Task<User> CreateUser(User user)
        {
            ValidateErrors(user);
            NormalizeData(user);
            SetGifUser(user);

            return await _UserRepository.AddUser(user);

        }
        public async Task<IEnumerable<User>> GetAllUser() => await _UserRepository.GetAllUser();
        public async Task<User> UpdateUser(User user)
        {
            ValidateErrors(user);
            NormalizeData(user);
            SetGifUser(user);

           return await _UserRepository.UpdateUser(user);
        }

        #region privateMethod
        private void ValidateErrors(User user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                throw new RecruitmentException("The name is required");
            }
            if (string.IsNullOrEmpty(user.Email))
            {
                throw new RecruitmentException("The email is required");
            }
            if (string.IsNullOrEmpty(user.Address))
            {
                throw new RecruitmentException("The address is required");
            }
            if (string.IsNullOrEmpty(user.Phone))
            {
                throw new RecruitmentException("The phone is required");
            }
        }
        private void NormalizeData(User user)
        {
            var aux = user.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            user.Email = string.Join("@", new string[] { aux[0], aux[1] });
        }
        private void SetGifUser(User user)
        {
            switch (user.UserType)
            {
                case (int)UserType.Normal:
                    if (user.Money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        var gif = user.Money * percentage;
                        user.Money = user.Money + gif;
                    }
                    else if (user.Money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = user.Money * percentage;
                        user.Money = user.Money + gif;

                    }
                    break;
                case (int)UserType.SuperUser:
                    if (user.Money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        var gif = user.Money * percentage;
                        user.Money = user.Money + gif;
                    }
                    break;
                case (int)UserType.Premium:
                    if (user.Money > 100)
                    {
                        var gif = user.Money * 2;
                        user.Money = user.Money + gif;
                    }
                    break;
                default:
                    throw new Exception("The user type do not annealed.");

            }
        }
        #endregion
    }
}
