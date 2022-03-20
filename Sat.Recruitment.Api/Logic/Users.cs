using System;
using System.Collections.Generic;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Controllers.Entity;
using Sat.Recruitment.Api.Controllers.Interface;
using Sat.Recruitment.Api.Data;
using Sat.Recruitment.Api.Logic.Entity;
using Sat.Recruitment.Api.Logic.Interface;

namespace Sat.Recruitment.Api.Logic
{
    public class Users : Ilogic
    {
        IbdUser bdUser = new DBuser();
        Ivalidate validate = new Validate();
        public Result CreateUser(RequestUser request)
        {
            try
            {
                User user = new User();
                if (request.UserType == "Normal")
                {
                    user.setValues(request);
                }
                else if (request.UserType == "SuperUser")
                {
                    user = new SuperUser();
                    user.setValues(request);
                }
                else if (request.UserType == "Premium")
                {
                    user = new PremiumUser();
                    user.setValues(request);
                }
                else
                {
                    throw new InvalidOperationException("Invalid UserType");
                }
                List<User> users = bdUser.getUsers();

                bool val = validate.validateDataUser(users, user);

                if (val == false)
                {
                    return new Result(val, "User is duplicated");
                }
                else
                {
                    //regisUser();
                    return new Result(val, "User Created");
                }
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }
    }
}
