using System.Net;
using Sat.Rec.Core.Repository.Interfaces;
using Sat.Rec.Models;

namespace Sat.Rec.Core.Validation
{
    public static class UserValidationExtension
    {
        public static ValidationResult<User> ValidateCreate(this User user, IUnitOfWork unitOfWork)
        {
            var validationResult = new ValidationResult<User>();

            //Normalize email
            user.Email = NormalizeEmail(user.Email);

            //First check non-database operations
            if (string.IsNullOrWhiteSpace(user.Address))
            {
                validationResult.Errors.Add("Address cannot be Empty");
            }
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                validationResult.Errors.Add("Email cannot be Empty");
            }
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                validationResult.Errors.Add("Name cannot be Empty");
            }
            if (string.IsNullOrWhiteSpace(user.Phone))
            {
                validationResult.Errors.Add("Phone cannot be Empty");
            }

            if (user.UserTypeId < 1)
            {
                validationResult.Errors.Add("UserTypeId cannot be less than '1' One");
            }

            //Now we check database dependant operations

            if (user.UserTypeId >= 1 && unitOfWork.UserTypes.GetById(user.UserTypeId).Result == null)
            {
                validationResult.Errors.Add("UserTypeId has invalid value (FK)");
            }

            if (!string.IsNullOrEmpty(user.Address) 
                && unitOfWork.Users.GetByAddress(user.Address).Result != null)
            {
                validationResult.Errors.Add("Unique Key validation for 'Address'");
            }
            if (!string.IsNullOrEmpty(user.Email)
                && unitOfWork.Users.GetByEmail(user.Email).Result != null)
            {
                validationResult.Errors.Add("Unique Key validation for 'Email'");
            }
            if (!string.IsNullOrEmpty(user.Name)
                && unitOfWork.Users.GetByName(user.Name).Result != null)
            {
                validationResult.Errors.Add("Unique Key validation for 'Name'");
            }
            if (!string.IsNullOrEmpty(user.Phone)
                && unitOfWork.Users.GetByPhone(user.Phone).Result != null)
            {
                validationResult.Errors.Add("Unique Key validation for 'Phone'");
            }

            if (validationResult.Errors.Count > 0)
            {
                validationResult.CustomResultCode = (int)HttpStatusCode.BadRequest;
            }

            return validationResult;
        }

        public static ValidationResult<User> ValidateUpdate(this User user, IUnitOfWork unitOfWork)
        {
            var validationResult = new ValidationResult<User>();
            if (user.UserId < 1)
            {
                validationResult.Errors.Add("UserId cannot be less than '1' One");
            }

            //Normalize email
            user.Email = NormalizeEmail(user.Email);

            //First check non-database operations
            if (string.IsNullOrWhiteSpace(user.Address))
            {
                validationResult.Errors.Add("Address cannot be Empty");
            }
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                validationResult.Errors.Add("Email cannot be Empty");
            }
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                validationResult.Errors.Add("Name cannot be Empty");
            }
            if (string.IsNullOrWhiteSpace(user.Phone))
            {
                validationResult.Errors.Add("Phone cannot be Empty");
            }

            if (user.UserTypeId < 1)
            {
                validationResult.Errors.Add("UserTypeId cannot be less than '1' One");
            }

            //Now we check database dependant operations
            if (unitOfWork.UserTypes.GetById(user.UserTypeId).Result == null)
            {
                validationResult.Errors.Add("UserTypeId has invalid value (FK)");
            }

            var userWithExistingAddress = unitOfWork.Users.GetByAddress(user.Address).Result;
            if (userWithExistingAddress != null && userWithExistingAddress.UserId != user.UserId)
            {
                validationResult.Errors.Add("Unique Key validation for 'Address'");
            }

            var userWithExistingEmail = unitOfWork.Users.GetByEmail(user.Email).Result;
            if (userWithExistingEmail != null && userWithExistingEmail.UserId != user.UserId)
            {
                validationResult.Errors.Add("Unique Key validation for 'Email'");
            }

            var userWithExistingName = unitOfWork.Users.GetByName(user.Name).Result;
            if (userWithExistingName != null && userWithExistingName.UserId != user.UserId)
            {
                validationResult.Errors.Add("Unique Key validation for 'Name'");
            }

            var userWithExistingPhone = unitOfWork.Users.GetByPhone(user.Phone).Result;
            if (userWithExistingPhone != null && userWithExistingPhone.UserId != user.UserId)
            {
                validationResult.Errors.Add("Unique Key validation for 'Phone'");
            }

            if (validationResult.Errors.Count > 0)
            {
                validationResult.CustomResultCode = (int)HttpStatusCode.BadRequest;
            }

            return validationResult;
        }

        private static string NormalizeEmail(string email)
        {
            if (email == null)
            {
                return email;
            }
            var emailSplit = email.Split("@");
            emailSplit[0] = emailSplit[0].Replace(".", "").Replace("+", "");
            return string.Join("@", emailSplit);
        }
    }
}
