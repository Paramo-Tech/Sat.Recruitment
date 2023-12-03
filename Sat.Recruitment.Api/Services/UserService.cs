namespace Sat.Recruitment.Api.Services
{
    using System;
    using Sat.Recruitment.Api.Domain;
    using System.Collections.Generic;
    using Sat.Recruitment.Api.Exceptions;
    using Sat.Recruitment.Api.Repositories;

    public class UserService: IUserService
    {
        private IGiftRulesRepository _giftRulesRepository;
        private IUsersRepository _usersRepository;


        public UserService(IGiftRulesRepository giftRulesRepository, IUsersRepository usersRespository)
        {
            _giftRulesRepository = giftRulesRepository;
            _usersRepository = usersRespository;
        }


        private bool isUserDuplicated(User user)
        {
            return _usersRepository.exists(user);
        }

        private GiftRule getApplicableGiftRule(User user)
        {
            var giftRules = _giftRulesRepository.get();
            return giftRules.Find(r =>
                r.userType == user.UserType && r.from < user.Money && (r.to == 0 || r.to > user.Money));
        }

        private decimal applyGiftCoefficient(decimal money, decimal coefficient)
        {
            return money * (coefficient + 1);
        }

        private string normalizeEmail(string email)
        {
            var emailParts = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            return emailParts[0].Replace(".", "") + "@" + emailParts[1];
        }

        public void createUser(User newUser)
        {
            newUser.Email = normalizeEmail(newUser.Email);

            if (isUserDuplicated(newUser)) throw new DuplicatedUserException();

            var applicableRule = getApplicableGiftRule(newUser);

            if (applicableRule != null)
                newUser.Money = applyGiftCoefficient(newUser.Money, applicableRule.coefficient);

            _usersRepository.create(newUser);
        }

        public List<User> getUsers(string name, string email, string address, string phone, string userType, string money)
        {
            return _usersRepository.get(name, email, address, phone, userType, money);
        }


    }
}
