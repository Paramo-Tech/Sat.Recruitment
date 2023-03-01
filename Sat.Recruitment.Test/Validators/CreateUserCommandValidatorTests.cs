using Application.Commands;
using Application.Validators;
using FluentValidation.TestHelper;
using Sat.Recruitment.Test.TestDoubles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Validators
{
    public class CreateUserCommandValidatorTests
    {
        CreateUserCommandValidator _createUserCommandValidator;

        public CreateUserCommandValidatorTests()
        {
            _createUserCommandValidator = new CreateUserCommandValidator();
        }

        [Fact]
        public async Task CreateUserCommandValidator_Should_Succeed()
        {
            TestValidationResult<CreateUserCommand> result = await _createUserCommandValidator.TestValidateAsync(FakeCreateUserCommand.WithValidData);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task CreateUserCommandValidator_Should_Fail_For_Empty_Name()
        {
            TestValidationResult<CreateUserCommand> result = await _createUserCommandValidator.TestValidateAsync(FakeCreateUserCommand.WithEmptyName);

            result.ShouldHaveValidationErrorFor(u => u.Name)
                .WithErrorMessage("The name is required");
        }
        [Fact]
        public async Task CreateUserCommandValidator_Should_Fail_For_Empty_Email()
        {
            TestValidationResult<CreateUserCommand> result = await _createUserCommandValidator.TestValidateAsync(FakeCreateUserCommand.WithEmptyEmail);

            result.ShouldHaveValidationErrorFor(u => u.Email)
                .WithErrorMessage("The email is required");
        }
        [Fact]
        public async Task CreateUserCommandValidator_Should_Fail_For_Email_With_Invalid_Format()
        {
            TestValidationResult<CreateUserCommand> result = await _createUserCommandValidator.TestValidateAsync(FakeCreateUserCommand.WithInvalidEmail);

            result.ShouldHaveValidationErrorFor(u => u.Email)
                .WithErrorMessage("The email is not valid");
        }

        [Fact]
        public async Task CreateUserCommandValidator_Should_Fail_For_Empty_Address()
        {
            TestValidationResult<CreateUserCommand> result = await _createUserCommandValidator.TestValidateAsync(FakeCreateUserCommand.WithEmptyAddress);

            result.ShouldHaveValidationErrorFor(u => u.Address)
                .WithErrorMessage("The address is required");
        }
        [Fact]
        public async Task CreateUserCommandValidator_Should_Fail_For_Empty_Phone()
        {
            TestValidationResult<CreateUserCommand> result = await _createUserCommandValidator.TestValidateAsync(FakeCreateUserCommand.WithEmptyPhone);

            result.ShouldHaveValidationErrorFor(u => u.Phone)
                .WithErrorMessage("The phone is required");
        }

        [Fact]
        public async Task CreateUserCommandValidator_Should_Fail_For_Negative_Money()
        {
            TestValidationResult<CreateUserCommand> result = await _createUserCommandValidator.TestValidateAsync(FakeCreateUserCommand.WithNegativeMoney);

            result.ShouldHaveValidationErrorFor(u => u.Money)
                .WithErrorMessage("The amount of money cannot be less than zero");
        }
    }
}
