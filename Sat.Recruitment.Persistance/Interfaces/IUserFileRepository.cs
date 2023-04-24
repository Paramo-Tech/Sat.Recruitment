using Sat.Recruitment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Persistence.Interfaces
{
    public interface IUserFileRepository
    {
        #region Methods
        StreamReader ReadUsersFromFile();
        bool SaveUserInFileAsync(string userLine);
        Task<List<UserE>> GetAllUsersAsync();
        #endregion
    }
}
