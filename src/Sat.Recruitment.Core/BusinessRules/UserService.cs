using Sat.Recruitment.Core.Abstractions.BusinessFeatures.GiftByUserType;
using Sat.Recruitment.Core.Abstractions.BusinessFeatures.NormalizeEmail;
using Sat.Recruitment.Core.Abstractions.Repositories;
using Sat.Recruitment.Core.Abstractions.Services;
using Sat.Recruitment.Core.DomainEntities;
using Sat.Recruitment.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<User> Create(User newUser)
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

            List<User> users = await _userRepository.GetAll(u => u.Email == newUser.Email || 
                                                            u.Phone == newUser.Phone ||
                                                            (u.Name == newUser.Name && u.Address == newUser.Address));

            if (users.Count > 0)
            {
                throw new EntityAlreadyExistsException(typeof(User).Name, "The user is duplicated.");
            }

            #endregion // Check duplicated user

            // Persist the new User
            await _userRepository.Add(newUser);

            return newUser;
        }

        public async Task<List<User>> GetAll(Func<User, bool> filter = null)
        {
            return await _userRepository.GetAll(filter);
        }

        public async Task<User> GetById(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            User user = await _userRepository.GetById(id);

            return user;
        }

        public async Task Delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            await _userRepository.Delete(user);
        }

        public async Task<User> Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Check if the Id of the User to be updated exists in the storage
            User exist = await GetById(user.Id);
            if (exist == null)
            {
                throw new EntityNotFoundException(typeof(User).Name, $"Searching with the Id = {user.Id}");
            }

            // Normalize email
            _normalizeEmail.Normalize(user.Email);

            // Check duplicated user
            List<User> users = await _userRepository.GetAll(u =>
                (u.Email == user.Email || u.Phone == user.Phone || (u.Name == user.Name && u.Address == user.Address)) && u.Id != user.Id);

            if (users.Count > 0)
            {
                throw new EntityAlreadyExistsException(typeof(User).Name, "The user is duplicated.");
            }

            // Persist the changes
            return await _userRepository.Update(user);
        }
    }
}
