using System;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Application.Interfaces;
using Sat.Recruitment.Api.Application.Request;
using Sat.Recruitment.Api.Middleware.Interfaces;
using Sat.Recruitment.Api.Services.Responses;

namespace Sat.Recruitment.Api.Application
{
    public class UserApplication:IUserApplication
    {
        private readonly IUserServices _UserServices;
        public UserApplication(IUserServices userServices)
        {
            _UserServices = userServices;
        }

        private string ValidateErrors(string name, string email, string address, string phone)
        {
            if (name == null)
                //Validate if Name is null
                return  "The name is required";
            if (email == null)
                //Validate if Email is null
                return  " The email is required";
            if (address == null)
                //Validate if Address is null
               return " The address is required";
            if (phone == null)
                //Validate if Phone is null
                return " The phone is required";

            return "Ok";
        }

        public Task<Result> AddUser(UserDTO request)
        {
            string Errors=ValidateErrors(request.Name, request.Email, request.Address, request.Phone);
            if (Errors != "Ok")
                throw new Exception(Errors);

            return _UserServices.AddUser(request);
        }
    }
}
