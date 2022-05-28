using System;
using Sat.Recruitment.Api.Domain.Services;
using Sat.Recruitment.Api.Domain.Services.Contracts;
using Sat.Recruitment.Domain.Contracts;

namespace Sat.Recruitment.Domain.Services
{
    public sealed class UserBuilderDirectorDefaultService : IUserBuilderDirectorService
    {
        private IUserModel _userModelBase;

        public User GetResult()
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