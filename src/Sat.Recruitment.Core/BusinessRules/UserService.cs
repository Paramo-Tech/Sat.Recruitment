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

        public async Task<User> Create(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Get Gift for the user depending on it UserType and amount of money
            decimal giftAmount = _giftByUserTypeMediator.GetGiftByUserType(user.UserType, user.Money);
            // Add the amount of the gift, to the initial amount of money
            user.Money = user.Money + giftAmount;

            // Normalize email
            user.Email = _normalizeEmail.Normalize(user.Email);

            // Check duplicated user
            List<User> users = await _userRepository.GetAll(u => u.Email == user.Email || 
                                                            u.Phone == user.Phone ||
                                                            (u.Name == user.Name && u.Address == user.Address));

            if (users.Count > 0)
            {
                throw new EntityAlreadyExistsException(typeof(User).Name, "The user is duplicated.");
            }

            // Persist the new User
            return await _userRepository.Add(user);
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

            // Get entity from storage
            User user = await _userRepository.GetById(id);

            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User).Name, $"Searching with the Id = {id}");
            }

            return user;
        }

        public async Task Delete(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            // Check if the Id of the User to be updated exists in the storage
            User exist = await _userRepository.GetById(id);
            if (exist == null)
            {
                throw new EntityNotFoundException(typeof(User).Name, $"Searching with the Id = {id}");
            }

            // Persis the deletion
            await _userRepository.Delete(id);
        }

        public async Task<User> Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Check if the Id of the User to be updated exists in the storage
            User exist = await _userRepository.GetById(user.Id);
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
