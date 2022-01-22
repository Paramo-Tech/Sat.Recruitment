using Sat.Recruitment.Entities;
using System;
using System.Threading.Tasks;

namespace Sat.Recruitment.Business.Contracts
{
    public interface IUserBusiness
    {
        Task<Result> CreateUser(User item);

        Func<User, bool> Filter(User item);

        bool Validate(User item, ref string errors);

    }

}
