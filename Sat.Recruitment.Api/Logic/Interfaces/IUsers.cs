using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Logic.Interfaces
{
    public interface IUsers
    {
        List<string> RequiredFieldsValidation(UserRequest user);

        Task<List<UserRequest>> ReadUsersFromFile();

        string NormalizeEmail(string email);

        decimal CalculateMoneyByUserType(string userType, decimal currentMoney);
    }
}
