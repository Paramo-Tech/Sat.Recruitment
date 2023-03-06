using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Users.Queries
{
    public static class GetUserQueryExtensions
    {
        public static GetUserDto MapTo(this User user)
        {
            return new GetUserDto
            {
                Name = user.Name,
                Address = user.Address,
                Email = user.Email,
                Money = user.Money,
                Phone = user.Phone,
                UserType = user.UserType
            };
        }
    }
}
