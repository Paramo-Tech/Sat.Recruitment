using Sat.Recruitment.Api.Models;

namespace Sat.Recruitment.Api.Services
{
    public interface IUserService
    {
        public bool IsDuplicated(User user);
        public void ApplyGift(User user);

    }
}
