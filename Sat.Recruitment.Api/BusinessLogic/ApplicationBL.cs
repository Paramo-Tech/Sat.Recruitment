using Sat.Recruitment.Api.BusinessLogic.Model;
using Sat.Recruitment.Api.Data;
using System.Linq;
using System.Collections.Generic;
using Sat.Recruitment.Api.BusinessLogic.Exceptions;
using System;

namespace Sat.Recruitment.Api.BusinessLogic
{
    public class ApplicationBL : IApplicationBL
    {
        private readonly IDataService _dataService;

        public ApplicationBL(IDataService dataService)
        {
            this._dataService = dataService; 
        }

        public List<User> GetUsers()
        {
            return this._dataService.GetUsers();
        }

        public User GetUser(Predicate<User> p)
        {
            return this._dataService.GetUserBy(p);
        }

        public void SaveUser(User user)
        {
            Validate(user);
            ApplyGift(user);
            this._dataService.Save(user);
        }

        private void ApplyGift(User user)
        {
            user.Money += user.Money * getGiftPercentage(user);
        }

        /// <summary>
        /// Return decimal gift percentage, depending on user properties
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private decimal getGiftPercentage(User user)
        {
            if (user.Money > 100)
            {
                if (user.UserType == User.NORMAL)
                    return 0.12M;
                else if (user.UserType == User.SUPERUSER)
                    return .2M;
                else if (user.UserType == User.PREMIUM)
                    return 2M;
            }
            else if (user.Money > 10)
            {
                if (user.UserType == User.NORMAL)
                    return .8M;
            }
            return 0;
        }

        /// <summary>
        /// Validate :
        ///    Doesn't exist other user with same Email or Phone
        ///    Doesn't exist other user with same Name and Address
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="EDuplicatedUserException"></exception>
        private void Validate(User user)
        {
            // same Email or Phone?
            if (this.GetUsers().Exists(u => u.Email == user.Email))
                throw new EDuplicatedUserException("Email");

            if (this.GetUsers().Exists(u => u.Phone == user.Phone))
                throw new EDuplicatedUserException("Phone");

            // same Name and Address
            if (this.GetUsers().Exists(u => u.Name == user.Name && u.Address == user.Address))
                throw new EDuplicatedUserException("Name and Address");
        }
    }
}
