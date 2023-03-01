using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.BusinessLogic
{
    public interface IUserBL
    {
        void ValidateGift(User user);
        void ValidateEmail(User user);
    }
}
