using Application.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Contracts.Validators
{
    public interface IUserCreationValidator
    {
        ValidationResult Validate(UserCreationDto userCreationDto);
    }
}