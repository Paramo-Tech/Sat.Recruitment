using Sat.Recruitment.Domain.Contracts.BonusCalculation;
using Sat.Recruitment.Domain.Contracts.Repositories;
using Sat.Recruitment.Domain.Contracts.Services;
using Sat.Recruitment.Domain.Model;
using Sat.Recruitment.Domain.BonusCalculation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Services
{
    public class UserService : IUserService
    {


        public IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {

            _userRepository = userRepository;
        }
       
        private void AddingBonus(User newUser)
        {

            IBonusCalculation bonusCalculation = null;

            switch (newUser.UserType)
            {
                case "Normal":
                    bonusCalculation = new BonusCalculationUserNormal();
                    break;
                case "SuperUser":
                    bonusCalculation = new BonusCalculationSuperUser();
                    break;
                case "Premiun":
                    bonusCalculation = new BonusCalculationPremiun();
                    break;
            }
            if (bonusCalculation != null)
            {
                newUser.Money = bonusCalculation.CalculateBonus(newUser);
            }
        }

        public async Task<ICollection<User>> GetUsersAsync()
        {

            return await _userRepository.GetUsers();
        }

        public async Task AddAsync(User user)
        {
            AddingBonus(user);
            await _userRepository.AddUser(user);
        }

        public async Task<bool> ExistsAsync(User newUser)
        {
            var isDuplicated = false;
            var users = await GetUsersAsync();
            foreach (var user in users)
            {
                if ((user.Email.ToLower() == newUser.Email.ToLower() || user.Phone == newUser.Phone) ||
                    (user.Name.ToLower() == newUser.Name.ToLower() && user.Address.ToLower() == newUser.Address.ToLower()))

                {
                    isDuplicated = true;
                }
            }

            return isDuplicated;
        }


    }
}
