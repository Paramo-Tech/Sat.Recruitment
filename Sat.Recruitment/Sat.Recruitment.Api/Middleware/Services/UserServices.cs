using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Sat.Recruitment.Api.Application.Request;
using Sat.Recruitment.Api.Middleware.Interfaces;
using Sat.Recruitment.Api.Services.Responses;

namespace Sat.Recruitment.Api.Middleware.Services
{
    public class UserServices:IUserServices
    {
        private Logs.Logs _logs;
        private List<UserDTO> _users;
        private readonly  IFileServices _fileServices;
        public UserServices(IFileServices fileServices,IWebHostEnvironment environment)
        {
            _users = new List<UserDTO>();
            _fileServices = fileServices;
            _logs = new Logs.Logs(environment);
            _logs.Lineas = new List<string>();
            _logs.Proceso = "USERS";
        }

        public async Task<Result> AddUser(UserDTO newUser)
        {
            Result result=new Result();
          
            var reader = _fileServices.ReadUsersFromFile();
            try
            {
                NormalizeEmail(ref newUser);
                _users = await _fileServices.ReadFileUsers(reader);
                _logs.Lineas.Add($" users  {JsonConvert.SerializeObject(_users)} ");
                _logs.GrabarLogs();
                if (UserExistFile(newUser))
                {
                    result.IsSuccess = false;
                    result.Errors = "User is duplicated";
                }
                else
                {
                    Debug.WriteLine("User Created");
                    result.IsSuccess = true;
                    result.Errors = "User Created";
                }

                _logs.Lineas.Add($" result validate exist userfile  {JsonConvert.SerializeObject(result)}");
                _logs.GrabarLogs();
            }
            catch (Exception e)
            {
                reader.Close();
                result.IsSuccess = false;
                result.Errors = e.Message;

            }
           
            return result;
        }
     
        private bool UserExistFile(UserDTO newUser)
        {
            foreach (var user in _users)
            {
                if (user.Email == newUser.Email || user.Phone == newUser.Phone)
                {
                    return true;
                }
                else if (user.Name == newUser.Name)
                {
                    if (user.Address == newUser.Address)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static void NormalizeEmail(ref UserDTO newUser)
        {
            var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
