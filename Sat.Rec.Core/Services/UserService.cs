using System.Net;
using Sat.Rec.Core.Repository.Interfaces;
using Sat.Rec.Core.Services.Interfaces;
using Sat.Rec.Core.Validation;
using Sat.Rec.Models;

namespace Sat.Rec.Core.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidationResult<User>> Create(User user)
        {
            var validationResult = user.ValidateCreate(_unitOfWork);

            if (validationResult.Errors.Any())
            {
                return validationResult;
            }
            try
            {
                //Apply GIF
                var userType = await _unitOfWork.UserTypes.GetById(user.UserTypeId);
                var gifByTypeId = await _unitOfWork.GIFUserTypes.GetAllByUserTypeId(user.UserTypeId);

                var gif = gifByTypeId.FirstOrDefault(x => x.LowerLimit < user.Money && user.Money < x.UpperLimit);
                if (gif != null)
                {
                    user.Money *= (1 + gif.GIF);
                }

                await _unitOfWork.Users.Add(user);

                var result = _unitOfWork.Save();

                validationResult.SingleResult = user;
            }
            catch (Exception ex)
            {
                validationResult.CustomResultCode = (int)HttpStatusCode.InternalServerError;
                validationResult.Errors.Add(ex.Message);
            }

            return validationResult;
        }

        public async Task<ValidationResult<User>> Delete(int id)
        {
            var validationResult = new ValidationResult<User>();
            if (id < 1)
            {
                validationResult.Errors.Add("UserId cannot be less than '1' One");
                validationResult.CustomResultCode = (int)HttpStatusCode.BadRequest;
                return validationResult;
            }
            try
            {
                var user = await _unitOfWork.Users.GetById(id);
                if (user != null)
                {
                    _unitOfWork.Users.Delete(user);
                    _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                validationResult.CustomResultCode = (int)HttpStatusCode.InternalServerError;
                validationResult.Errors.Add(ex.Message);
            }

            return validationResult;
        }

        public async Task<ValidationResult<User>> GetAll()
        {
            var validationResult = new ValidationResult<User>();
            try
            {
                var usersList = await _unitOfWork.Users.GetAll();
                validationResult.ResultList = usersList.ToList();
                validationResult.CustomResultCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                validationResult.CustomResultCode = (int)HttpStatusCode.InternalServerError;
                validationResult.Errors.Add(ex.Message);
            }
            return validationResult;
        }

        public async Task<ValidationResult<User>> GetById(int id)
        {
            var validationResult = new ValidationResult<User>();
            if (id < 1)
            {
                validationResult.Errors.Add("UserId cannot be less than '1' One");
                validationResult.CustomResultCode = (int)HttpStatusCode.BadRequest;
                return validationResult;
            }
            try
            {
                var user = await _unitOfWork.Users.GetById(id);
                if (user == null)
                {
                    validationResult.CustomResultCode = (int)HttpStatusCode.NotFound;
                    return validationResult;
                }
                validationResult.SingleResult = user;
                validationResult.CustomResultCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                validationResult.CustomResultCode = (int)HttpStatusCode.InternalServerError;
                validationResult.Errors.Add(ex.Message);
            }
            return validationResult;
        }

        public async Task<ValidationResult<User>> Update(User newUserVersion)
        {
            var validationResult = newUserVersion.ValidateUpdate(_unitOfWork);
            if (validationResult.Errors.Any())
            {
                return validationResult;
            }
            try
            {
                var currentUserVersion = await _unitOfWork.Users.GetById(newUserVersion.UserId);
                if (currentUserVersion == null)
                {
                    validationResult.Errors.Add("Wrong Id");
                    validationResult.CustomResultCode = (int)HttpStatusCode.BadRequest;
                }
                else
                {
                    currentUserVersion.Address = newUserVersion.Address;
                    currentUserVersion.Email = newUserVersion.Email;
                    currentUserVersion.Money = newUserVersion.Money;
                    currentUserVersion.Name = newUserVersion.Name;
                    currentUserVersion.Phone = newUserVersion.Phone;
                    currentUserVersion.UserTypeId = newUserVersion.UserTypeId;

                    _unitOfWork.Users.Update(currentUserVersion);

                    _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                validationResult.CustomResultCode = (int)HttpStatusCode.InternalServerError;
                validationResult.Errors.Add(ex.Message);
            }

            return validationResult;
        }


    }
}
