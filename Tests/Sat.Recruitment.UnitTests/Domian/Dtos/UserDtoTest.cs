using Moq;
using Sat.Recruitment.CommonTests.Builders.Dtos;
using Sat.Recruitment.CommonTests.TestsBases;
using Sat.Recruitment.Domain.Dtos;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.UnitTests.Domian.Dtos
{
    public class UserDtoTests : DtoTestsBase
    {
        private UserDto _dto;

        public UserDtoTests()
        {
            _dto = UserDtoBuilder.BuildInstance();
        }

        [Fact]
        public Task Validate_UserBuilder_ReturnCorrectUserDto()
        {
            //Arrange
            //The dto was define in the Test Contructor.

            //Act
            //The dto was instance in the Test Contructor.

            //Assert
            Assert.NotNull(_dto);
            Assert.NotNull(_dto.Name);
            Assert.NotNull(_dto.Email);
            Assert.NotNull(_dto.Address);
            Assert.NotNull(_dto.Phone);

            Assert.NotNull(_dto.UserType);
            Assert.NotNull(_dto.Money);
            Assert.NotNull(_dto.Password);

            return Task.CompletedTask;
        }
       
        [Fact]
        public async Task Validate_WithUserOk_ReturnNoErros()
        {
            //Arrange
            //The dto was define in the Test Contructor.

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.Empty(result);

        }

        [Fact]
        public async Task Validate_WithUserEmpty_ReturnConcatErros()
        {
            //Arrange
            _dto = new UserDto();

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
            Assert.Contains(nameof(_dto.Name), result, System.StringComparison.OrdinalIgnoreCase);
            Assert.Contains(nameof(_dto.Email), result, System.StringComparison.OrdinalIgnoreCase);
            Assert.Contains(nameof(_dto.Address), result, System.StringComparison.OrdinalIgnoreCase);
            Assert.Contains(nameof(_dto.Phone), result, System.StringComparison.OrdinalIgnoreCase);
            Assert.Contains(nameof(_dto.Password), result, System.StringComparison.OrdinalIgnoreCase);

        }

        #region Validate Name 

        [Fact]
        public async Task Validate_NameNull_ReturnError()
        {
            //Arrange
            _dto.Name = null;

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
            Assert.Contains(nameof(_dto.Name),result,System.StringComparison.OrdinalIgnoreCase);

        }

        [Fact]
        public async Task Validate_NameEmpty_ReturnError()
        {
            //Arrange
            _dto.Name = string.Empty;

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
           Assert.Contains(nameof(_dto.Name),result,System.StringComparison.OrdinalIgnoreCase);

        }
        
        [Fact]
        public async Task Validate_NameWithQuotationMarks_ReturnError()
        {
            //Arrange
            _dto.Name = "";

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
            Assert.Contains(nameof(_dto.Name), result, System.StringComparison.OrdinalIgnoreCase);

        }

        [Fact]
        public async Task Validate_NameWithWhiteSpaces_ReturnError()
        {
            //Arrange
            _dto.Name = " ";

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
           Assert.Contains(nameof(_dto.Name),result,System.StringComparison.OrdinalIgnoreCase);

        }


        #endregion

        #region Validate Email 

        [Fact]
        public async Task Validate_EmailNull_ReturnError()
        {
            //Arrange
            _dto.Email = null;

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
           Assert.Contains(nameof(_dto.Email),result,System.StringComparison.OrdinalIgnoreCase);

        }

        [Fact]
        public async Task Validate_EmailEmpty_ReturnError()
        {
            //Arrange
            _dto.Email = string.Empty;

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
           Assert.Contains(nameof(_dto.Email),result,System.StringComparison.OrdinalIgnoreCase);

        }
        
        [Fact]
        public async Task Validate_EmailWithQuotationMarks_ReturnError()
        {
            //Arrange
            _dto.Email = "";

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
            Assert.Contains(nameof(_dto.Email), result, System.StringComparison.OrdinalIgnoreCase);

        }

        [Fact]
        public async Task Validate_EmailWithWhiteSpaces_ReturnError()
        {
            //Arrange
            _dto.Email = " ";

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
           Assert.Contains(nameof(_dto.Email),result,System.StringComparison.OrdinalIgnoreCase);

        }

        #endregion

        #region Validate Address 

        [Fact]
        public async Task Validate_AddressNull_ReturnError()
        {
            //Arrange
            _dto.Address = null;

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
           Assert.Contains(nameof(_dto.Address),result,System.StringComparison.OrdinalIgnoreCase);

        }

        [Fact]
        public async Task Validate_AddressEmpty_ReturnError()
        {
            //Arrange
            _dto.Address = string.Empty;

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
           Assert.Contains(nameof(_dto.Address), result, System.StringComparison.OrdinalIgnoreCase);

        }

        [Fact]
        public async Task Validate_AddressWithQuotationMarks_ReturnError()
        {
            //Arrange
            _dto.Address = "";

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
            Assert.Contains(nameof(_dto.Address), result, System.StringComparison.OrdinalIgnoreCase);

        }

        [Fact]
        public async Task Validate_AddressWithWhiteSpaces_ReturnError()
        {
            //Arrange
            _dto.Address = "";

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
           Assert.Contains(nameof(_dto.Address),result,System.StringComparison.OrdinalIgnoreCase);

        }

        #endregion

        #region Validate Phone 

        [Fact]
        public async Task Validate_PhoneNull_ReturnError()
        {
            //Arrange
            _dto.Phone = null;

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
           Assert.Contains(nameof(_dto.Phone),result,System.StringComparison.OrdinalIgnoreCase);

        }

        [Fact]
        public async Task Validate_PhoneEmpty_ReturnError()
        {
            //Arrange
            _dto.Phone = string.Empty;

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
            Assert.Contains(nameof(_dto.Phone),result,System.StringComparison.OrdinalIgnoreCase);

        }

        [Fact]
        public async Task Validate_PhoneWithQuotationMarks_ReturnError()
        {
            //Arrange
            _dto.Phone = "";

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
            Assert.Contains(nameof(_dto.Phone), result, System.StringComparison.OrdinalIgnoreCase);

        }

        [Fact]
        public async Task Validate_PhoneWithWhiteSpaces_ReturnError()
        {
            //Arrange
            _dto.Phone = " ";

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
           Assert.Contains(nameof(_dto.Phone),result,System.StringComparison.OrdinalIgnoreCase);

        }

        #endregion

        #region Validate Password 

        [Fact]
        public async Task Validate_PasswordNull_ReturnError()
        {
            //Arrange
            _dto.Password = null;

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
           Assert.Contains(nameof(_dto.Password),result,System.StringComparison.OrdinalIgnoreCase);

        }

        [Fact]
        public async Task Validate_PasswordEmpty_ReturnError()
        {
            //Arrange
            _dto.Password = string.Empty;

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
           Assert.Contains(nameof(_dto.Password),result,System.StringComparison.OrdinalIgnoreCase);

        }

        [Fact]
        public async Task Validate_PasswordWithQuotationMarks_ReturnError()
        {
            //Arrange
            _dto.Password = "";

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
            Assert.Contains(nameof(_dto.Password), result, System.StringComparison.OrdinalIgnoreCase);

        }

        [Fact]
        public async Task Validate_PasswordWithWhiteSpaces_ReturnError()
        {
            //Arrange
            _dto.Password = " ";

            //Act
            var result = await _dto.ValidateDto();

            //Assert
            Assert.NotNull(result);
           Assert.Contains(nameof(_dto.Password),result,System.StringComparison.OrdinalIgnoreCase);

        }

        #endregion

    }
}

