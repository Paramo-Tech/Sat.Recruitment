using Sat.Recruitment.Core.Abstractions.BusinessFeatures.GiftByUserType;
using Sat.Recruitment.Core.Abstractions.BusinessFeatures.NormalizeEmail;
using Sat.Recruitment.Core.Abstractions.Repositories;
using Sat.Recruitment.Core.Abstractions.Services;
using Sat.Recruitment.Core.DomainEntities;
using Sat.Recruitment.Core.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core.BusinessRules
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGiftByUserTypeMediator _giftByUserTypeMediator;
        private readonly INormalizeEmail _normalizeEmail;

        public UserService(IUserRepository userRepository, IGiftByUserTypeMediator giftByUserTypeMediator, INormalizeEmail normalizeEmail)
        {
            this._userRepository = userRepository;
            this._giftByUserTypeMediator = giftByUserTypeMediator;
            this._normalizeEmail = normalizeEmail;
        }

        public Result Create(User newUser)
        {
            #region Gift functionality

            // Get Gift for the user depending on it UserType and amount of money
            decimal giftAmount = _giftByUserTypeMediator.GetGiftByUserType(newUser.UserType, newUser.Money);

            // Add the amount of the gift, to the initial amount of money
            newUser.Money = newUser.Money + giftAmount;

            #endregion // Gift functionality

            #region Normalize email

            newUser.Email = _normalizeEmail.Normalize(newUser.Email);

            #endregion // Normalize email

            #region Check duplicated user

            List<User> users = _userRepository.GetAll(u => u.Email == newUser.Email || 
                                                           u.Phone == newUser.Phone ||
                                                           (u.Name == newUser.Name && u.Address == newUser.Address));

            try
            {
                var isDuplicated = false;

                if (users.Count > 0)
                {
                    isDuplicated = true;
                }

                if (!isDuplicated)
                {
                    Debug.WriteLine("User Created");

                    return new Result()
                    {
                        IsSuccess = true,
                        Errors = "User Created"
                    };
                }
                else
                {
                    Debug.WriteLine("The user is duplicated");

                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }
            }
            catch
            {
                Debug.WriteLine("The user is duplicated");
                return new Result()
                {
                    IsSuccess = false,
                    Errors = "The user is duplicated"
                };
            }

            #endregion // Check duplicated user

            return new Result()
            {
                IsSuccess = true,
                Errors = "User Created"
            };
        }
    }
}
