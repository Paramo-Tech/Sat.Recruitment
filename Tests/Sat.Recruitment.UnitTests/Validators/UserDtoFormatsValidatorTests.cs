using Sat.Recruitment.CommonTests;
using Sat.Recruitment.CommonTests.Builders;
using Sat.Recruitment.CommonTests.Builders.Dtos;
using Sat.Recruitment.CommonTests.TestsBases;
using Sat.Recruitment.Domain.Dtos;
using Sat.Recruitment.Domain.Validators;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.UnitTests.Validators
{
    public class UserDtoFormatsValidatorTests : ValidatorTestsBase
    {
        private readonly UserDto _dto;
        private readonly UserDtoFormatsValidator _validator;

        public UserDtoFormatsValidatorTests()
        {
            _dto = UserDtoBuilder.BuildInstance();
            _validator = new UserDtoFormatsValidator();
        }

        [Fact]
        public async Task Validate_ValidEntity_ReturnsNoErrors()
        {
            //Arrange
            //All the properties have been set with valid values in the Builder.

            //Act
            var fluentValidationResult = await _validator.ValidateAsync(_dto);

            //Assert
            Validate_Ok(fluentValidationResult);
        }

        #region Validate Email

        [Fact]
        public async Task Validate_EmailWithValidFormat_ReturnsOk()
        {
            //Arrange
            _dto.Email = "email@gmail.com";

            //Act
            var fluentValidationResult = await _validator.ValidateAsync(_dto);

            //Assert
            Validate_Ok(fluentValidationResult);
        }

        [Fact]
        public async Task Validate_Email_WithInvalidFormat_ReturnsError()
        {
            //Arrange
            _dto.Email = TestsHelper.GenerateRandomString(10);

            //Act
            var fluentValidationResult = await _validator.ValidateAsync(_dto);

            //Assert
            Validate_Error(fluentValidationResult,nameof(_dto.Email));
        }

        #endregion

        #region Validate UserType

        [Fact]
        public async Task Validate_UserType_ValidateUserTypeIsEnum_ReturnOk()
        {
            //Arrange
            _dto.UserType = SharedValues.User_UserType;

            //Act
            var fluentValidationResult = await _validator.ValidateAsync(_dto);

            //Assert
            Validate_Ok(fluentValidationResult);
        }

        [Fact]
        public async Task Validate_UserType_ValidateUserTypeIsEnum_ReturnError()
        {
            //Arrange
            _dto.UserType = SharedValues.User_InvalidUserType;

            //Act
            var fluentValidationResult = await _validator.ValidateAsync(_dto);

            //Assert
            Validate_Error(fluentValidationResult, $"{nameof(_dto.UserType)}");
        }

        #endregion

        #region Validate Money

        [Fact]
        public async Task Validate_Money_ValidateMoneyIsDecimal_ReturnOk()
        {
            //Arrange
            _dto.Money = SharedValues.User_Money;

            //Act
            var fluentValidationResult = await _validator.ValidateAsync(_dto);

            //Assert
            Validate_Ok(fluentValidationResult);
        }

        [Fact]
        public async Task Validate_UserType_ValidateMoneyIsDecimal_ReturnError()
        {
            //Arrange
            _dto.Money = SharedValues.User_InvalidMoney;

            //Act
            var fluentValidationResult = await _validator.ValidateAsync(_dto);

            //Assert
            Validate_Error(fluentValidationResult, $"{nameof(_dto.Money)}");
        }

        #endregion
    }
}
