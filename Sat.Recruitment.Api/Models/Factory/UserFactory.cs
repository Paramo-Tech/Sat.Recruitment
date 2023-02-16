using Sat.Recruitment.Api.Models.DTO;
using Sat.Recruitment.Api.Models.Interfaces;
using Sat.Recruitment.Api.Models.Users;

namespace Sat.Recruitment.Api.Models.Factory
{
    public class UserFactory : IUserFactory
    {
        public IUser CreateUser(UserDTO dto)
        {
            switch (dto.UserType)       
            {
                case UserTypes.NORMAL:
                    return new NormalUser
                    {
                        Name = dto.Name,
                        Email = dto.Email,
                        Address = dto.Address,
                        Phone = dto.Phone,
                        Money = dto.Money
                    };

                case UserTypes.SUPER:
                    return new SuperUser
                    {
                        Name = dto.Name,
                        Email = dto.Email,
                        Address = dto.Address,
                        Phone = dto.Phone,
                        Money = dto.Money
                    };

                case UserTypes.PREMIUM:
                    return new PremiumUser
                    {
                        Name = dto.Name,
                        Email = dto.Email,
                        Address = dto.Address,
                        Phone = dto.Phone,
                        Money = dto.Money
                    };
                default:
                    return null;
            }
        }
    }
}
