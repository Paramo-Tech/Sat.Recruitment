using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Dtos;
using System;
using Sat.Recruitment.Api.Helpers;
using Sat.Recruitment.Api.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IFileHandlerService _fileHandlerService;
        public UserBusiness(IFileHandlerService fileHandlerService) 
        {
            _fileHandlerService = fileHandlerService;
        }

        public async Task<ResultResponse> ProcessCreateUser(UserDto user)
        {
            var response = new ResultResponse() { IsSuccess = true, Message = "User created." };

            try
            {
                UserHelper.UpdateMoneyByUserType(user);
                UserHelper.NormalizeEmail(user);

                var existingUsers = await GetExistingUsers();
                if (UserHelper.IsUserExisting(user, existingUsers)) throw new Exception("User already exists.");    

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<List<UserDto>> GetExistingUsers() 
        {
            var usersTxtFile = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            var linesFromFile = _fileHandlerService.GetTxtFileLines(usersTxtFile);

            return await UserHelper.GetUsersByStringLines(linesFromFile);
        }

    }
}
