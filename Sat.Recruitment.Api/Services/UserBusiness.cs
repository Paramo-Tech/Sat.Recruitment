using Sat.Recruitment.Api.Integration;
using Sat.Recruitment.Api.Entitys;
using Sat.Recruitment.Api.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Utils;

namespace Sat.Recruitment.Api.Services
{
    public class UserBusiness: IUserBussiness
    {

        async Task<Result> IUserBussiness.AddUser(User newUser)
        {
            GiftContext giftContext = new GiftContext();
            String errors = ValidateErrors(newUser.Name, newUser.Email, newUser.Address, newUser.Phone);
            if (errors.Equals(""))
            {
                newUser.Money += giftContext.GetPercentaje(newUser.UserType, newUser.Money);
                
                newUser.Email = NormaliceEmail.Normalice(newUser.Email);//Normalize email
                try
                {
                    Boolean isDuplicated = await ValidDuplicated(newUser);
                    if (isDuplicated)
                        return new Result(false, "The user is duplicated");
                    else
                        return new Result(true, "User Created");
                }
                catch (Exception e)
                {
                    return new Result(false, e.Message);
                }
            }
            else
                return new Result(false, errors);
        }

        private async Task<bool> ValidDuplicated(User newUser)
        {
            UserRepository userRepository = new UserRepository();
            List<User> _users = (List<User>)await userRepository.LoadUsersAsync();
            List<User> oldUser = _users.FindAll(x =>
                                    (x.Email == newUser.Email || x.Phone == newUser.Phone)
                                    || (x.Name == newUser.Name && x.Address == newUser.Address));
            return oldUser.Count > 0;
        }

        private string ValidateErrors(string name, string email, string address, string phone)
        {
            string errors = "";
            if (name == null || name == "")
                errors = "The name is required";
            if (email == null || email == "")
                errors += " The email is required";
            if (address == null || address == "")
                errors += " The address is required";
            if (phone == null || phone == "")
                errors += " The phone is required";
            return errors;
        }
    }
}
