using FluentValidation;
using Sat.Recruitment.Core.Abstractions.BusinessFeatures.GiftByUserType;
using Sat.Recruitment.Core.Abstractions.BusinessFeatures.NormalizeEmail;
using Sat.Recruitment.Core.Abstractions.Repositories;
using Sat.Recruitment.Core.BusinessRules;
using Sat.Recruitment.Core.BusinessRules.Features.GiftByUserType;
using Sat.Recruitment.Core.BusinessRules.Features.NormalizeEmail;
using Sat.Recruitment.Core.DomainEntities;
using Sat.Recruitment.Core.Enums;
using Sat.Recruitment.Core.Validators;
using Sat.Recruitment.Infrastructure.TextFile.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Core.IntegrationTests
{
    /// <summary>
    /// These tests will use concrete implementations and not mocks, since
    /// their purpose is to test end-to-end functionality.
    /// 
    /// The implementations of this tests correspond to the following
    /// assemblies:
    /// - Sat.Recruitment.Core
    /// - Sat.Recruitment.Infrastructure.TextFile
    /// </summary>
    public class UserServiceWithTextFile
    {
        [Fact]
        public async Task Create_WithUnexistingUser_ReturnsNewUser()
        {
            // Arrange
            IUserRepository userRepository = new UserRepository();

            IValidator<User> validator = new UserValidator();

            INormalUserGiftStrategy normalUserGiftStrategy = new NormalUserGiftStrategy();
            IPremiumUserGiftTrategy premiumUserGiftTrategy = new PremiumUserGiftTrategy();
            ISuperUserGiftStrategy superUserGiftStrategy = new SuperUserGiftStrategy();

            IGiftByUserTypeMediator giftByUserTypeMediator = new GiftByUserTypeMediator(normalUserGiftStrategy, premiumUserGiftTrategy, superUserGiftStrategy);

            INormalizeEmail normalizeEmail = new NormalizeEmail();

            UserService userService = new UserService(userRepository, validator, giftByUserTypeMediator, normalizeEmail);

            User newUser = new User()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserType.Normal,
                Money = 124
            };

            // Act
            User persistedUser = await userService.Create(newUser);

            // Assert
            Assert.NotNull(persistedUser);
            Assert.True(!string.IsNullOrEmpty(persistedUser.Id.ToString()));
            Assert.Equal(persistedUser.Name, newUser.Name);
            Assert.Equal(persistedUser.Email, newUser.Email);
            Assert.Equal(persistedUser.Address, newUser.Address);
            Assert.Equal(persistedUser.Phone, newUser.Phone);
            Assert.Equal(persistedUser.UserType, newUser.UserType);
            Assert.Equal(138.88M, newUser.Money);
        }
    }
}
