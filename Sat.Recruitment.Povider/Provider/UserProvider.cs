using Sat.Recruitment.Model.Shared;
using Sat.Recruitment.Model.User;
using Sat.Recruitment.Povider.IProvider;
using Sat.Recruitment.Services.IServices;

namespace Sat.Recruitment.Povider.Provider
{
    public class UserProvider : IUserProvider
    {
        private const decimal PercentageNormal = 0.12m;
        private const decimal PercentageNormalMinimal = 0.8m;
        private const decimal PercentageSuperUser= 0.20m;
        private const decimal PercentagePremium = 2m;
        private readonly IService _service;

        public UserProvider(IService service)
        {
            _service = service;
        }
        public async Task<ResponseModel> CreateUser(UserModel user)
        {
            PercentagePerUserType(user);
            var users = await _service.GetUsers();
            if (users.Any(e => e.Email.Equals(user.Email) || e.Phone.Equals(user.Phone) || e.Name.Equals(user.Name) || e.Address.Equals(user.Address)))
            {
                return new ResponseModel()
                {
                    Error = new ErrorModel()
                    {
                        ErrorId = 002,
                        ErrorMessage = "User is duplicated"
                    },
                    IsSuccess = false
                };
            }
            return await _service.AddUser(user);
        }
        #region Helpers
        private void PercentagePerUserType(UserModel user)
        {
            switch (user.UserType)
            {
                case UserType.Normal:
                    user.Money = user.Money > 100 ? GetPercentageByUserType(user.Money, PercentageNormal) : user.Money > 10 ? GetPercentageByUserType(user.Money, PercentageNormalMinimal) : user.Money;
                    break;
                case UserType.SuperUser:
                    user.Money = user.Money > 100 ? GetPercentageByUserType(user.Money, PercentageSuperUser) : user.Money;
                    break;
                case UserType.Premium:
                    user.Money = user.Money > 100 ? GetPercentageByUserType(user.Money, PercentagePremium) : user.Money;
                    break;
            }
        }

        private decimal GetPercentageByUserType(decimal money, decimal percentage)
        {
            var gif = money * percentage;
            return money + gif;
        }
        
        #endregion
    }
}
