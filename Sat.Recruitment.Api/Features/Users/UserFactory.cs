using Sat.Recruitment.Api.Features.Common;

namespace Sat.Recruitment.Api.Features.Users
{
    public static class UserFactory
    {
        public static Result<UserBase> Create(
            UserType type,
            string name,
            string email,
            string address,
            string phone,
            decimal money)
        {
            var emailObject = Email.Create(email);
            switch (type)
            {
                case UserType.Normal:
                    return new Normal(name, emailObject, address, phone, money);
                case UserType.SuperUser:
                    return new SuperUser(name, emailObject, address, phone, money);
                case UserType.Premium:
                    return new Premium(name, emailObject, address, phone, money);
                default:
                    return Result.Failure<UserBase>($"Unexpected user type: {type}");
            }
        }
    }
}
