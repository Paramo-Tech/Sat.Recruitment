using Sat.Recruitment.BusinessLogic.ExternalServices;
using Sat.Recruitment.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataAccess _dataAccess;
        public UserRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<User> Get(Guid UserId)
        {
            List<User> usersList = await _dataAccess.ReadData();
            return usersList.Where(u => u.Id == UserId).FirstOrDefault();
        }

        public async Task<List<User>> GetAll()
        {
            return await _dataAccess.ReadData();
        }

        public async Task<bool> Save(User User)
        {
            try
            {
                var userList = await _dataAccess.ReadData();
                userList.Add(User);
                return await _dataAccess.SaveData(userList);
            }
            catch
            {
                return false;
            }
        }
    }
}