using Sat.Recruitment.Domain.Interfaces;
using Sat.Recruitment.Domain.Validators;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Domain.Dtos
{
    public class UserDto : IValidableDto
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        public string UserType { get; set; }

        public string Money { get; set; }

        [Required]
        public string Password { get; set; }
        
        public async Task<string> ValidateDto()
        {
            var errors = string.Empty;

            if (string.IsNullOrWhiteSpace(Name))
                errors = "The name is required.";

            if (string.IsNullOrWhiteSpace(Email))
                errors += " The email is required.";

            if (string.IsNullOrWhiteSpace(Address))
                errors += " The address is required.";

            if (string.IsNullOrWhiteSpace(Phone))
                errors += " The phone is required.";

            if (string.IsNullOrWhiteSpace(Password))
                errors += " The password is required.";

            var validator = new UserDtoFormatsValidator();

            FluentValidation.Results.ValidationResult fluentResult = await validator.ValidateAsync(this);
             
            if(!fluentResult.IsValid)
            {
                errors += string.Join("",fluentResult.Errors);
            }
            
            return await Task.FromResult(errors);
        }
    }

}
