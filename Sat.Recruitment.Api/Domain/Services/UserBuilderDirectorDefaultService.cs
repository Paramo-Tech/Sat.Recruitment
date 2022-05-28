using System;
using Sat.Recruitment.Api.Domain.Contracts;
using Sat.Recruitment.Api.Domain.Services.Contracts;

namespace Sat.Recruitment.Api.Domain.Services
{
    public sealed class UserBuilderDirectorDefaultService : IUserBuilderDirectorService
    {
        private IUserModel _userModelBase;

        public UserModel GetResult()
        {
            switch (_userModelBase.UserType)
            {
                case UserType.Normal:
                    return new NormalUserBuilder(_userModelBase).Build();
                case UserType.SuperUser:
                    return new SuperUserBuilder(_userModelBase).Build();

                case UserType.Premium:
                    return new PremiumUserBuilder(_userModelBase).Build();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void PrepareBuilder(IUserModel userModelBase)
        {
            this._userModelBase = userModelBase;
        }
    }
}