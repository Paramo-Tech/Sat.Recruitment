using Sat.Recruitment.Core.Abstractions.BusinessFeatures.GiftByUserType;
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

        public UserService(IUserRepository userRepository, IGiftByUserTypeMediator giftByUserTypeMediator)
        {
            this._userRepository = userRepository;
            this._giftByUserTypeMediator = giftByUserTypeMediator;
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

            /* After some quick research, I discovered that the use of the . and the + are
             * Gmail-specific implementations, and that there is no standard to support them,
             * so implementing the "Normalize Email" functionality to all email providers
             * could lead to the generation of invalid addresses.
             * 
             * An official source on Google Blog:
             *    https://gmail.googleblog.com/2008/03/2-hidden-ways-to-get-more-from-your.html
             *    
             * Then, the location of the dots matters for emails on Microsoft Outlook,
             * Yahoo Mail, and Apple iCloud, to mention some. Dots don’t matter for Facebook,
             * and they aren’t used at all for Twitter handles.
             */

            /* Takes an string and splits it on the @ character, if any of the partitions
             * result in an empty array, it removes them from the result.
             * 
             * Important fact here: if the supplied string is not a string of type
             * somestring@anotherstring the algorithm will fail at runtime without exception
             * handling.
             */
            var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            /* Finds the location of the first occurrence of the + sign, and returns it
             * in with respect to a zero-based array. If the sign is not found, then return -1.
             */
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            /* If the string does not contain a + sign: then it replaces occurrences of the
             * sign . with empty spaces.
             * 
             * If the string does contain a + sign: then it replaces occurrences of the sign .
             * with empty spaces, and remove everything to the right of the + sign (including
             * the + sign)
             * */
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            // Compose the email again with the two parts of the chain.
            newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });

            #endregion // Normalize email

            #region Check duplicated user

            List<User> users = _userRepository.GetAll();

            try
            {
                var isDuplicated = false;
                foreach (var user in users)
                {
                    if (user.Email == newUser.Email
                        ||
                        user.Phone == newUser.Phone)
                    {
                        isDuplicated = true;
                    }
                    else if (user.Name == newUser.Name)
                    {
                        if (user.Address == newUser.Address)
                        {
                            isDuplicated = true;
                            throw new Exception("User is duplicated");
                        }
                    }
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
