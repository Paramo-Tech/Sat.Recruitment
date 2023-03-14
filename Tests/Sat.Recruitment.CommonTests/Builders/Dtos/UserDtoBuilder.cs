using Sat.Recruitment.Domain.Dtos;

namespace Sat.Recruitment.CommonTests.Builders.Dtos
{
    public class UserDtoBuilder
    {
        protected UserDtoBuilder() { }

        public static UserDto BuildInstance()
        {

            var defaultInstance = new UserDto
            {

                Name = SharedValues.User_Name,
                Email = SharedValues.User_Email,
                Address = SharedValues.User_Address,
                Phone = SharedValues.User_Phone,
                UserType = SharedValues.User_UserType,
                Money = SharedValues.User_Money,
                Password = SharedValues.User_Password,
            };

            return defaultInstance;
        }
    }
}
