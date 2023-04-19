using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Core.Factories;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Tools;
using System.Threading.Tasks;

namespace Sat.Recruitment.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly string _success = "User Created";
        private readonly string _duplicated = "The User is duplicated";
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<string> Create(USER request)
        {
            //Normalize email
            request.Email = Utils.NormalizeEmail(request.Email);

            if (this.userRepository.IsUserDuplicated(request))
            {
                return _duplicated;
            }

            //calculate bonus
            var bonusCalculator = UserBonusCalculatorFactory.GetBonusCalculator(request.UserType);
            var bonus = bonusCalculator.CalculateBonus(request.Money);
            request.Money += bonus;

            this.userRepository.AddUser(request);
            return _success;
        }
    }
}
