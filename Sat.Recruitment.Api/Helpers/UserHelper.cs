using Sat.Recruitment.Api.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Helpers
{
    public static class UserHelper
    {
        internal static void UpdateMoneyByUserType(UserDto user)
        {
            var userMoney = user.Money;
            var percentage = Convert.ToDecimal(GetBonusPercentage(user.UserType, userMoney));

            user.Money = userMoney + (userMoney * percentage);
        }

        internal static void NormalizeEmail(UserDto user)
        {
            user.Email = user.Email.Trim().ToLower();
        }

        private static double GetBonusPercentage(string userType, decimal money)
        {
            double percentage = 0;

            switch (userType)
            {
                case "Normal":

                    if (money > 100)
                    {
                        percentage = 0.12;
                    }
                    else if (money > 10 && money != 100)
                    {
                        percentage = 0.8;
                    }

                    break;

                case "SuperUser":

                    percentage = 0.20;

                    break;

                case "Premium":

                    percentage = 2;

                    break;
            }

            return percentage;

        }

        internal static async Task<List<UserDto>> GetUsersByStringLines(List<string> linesFromFile)
        {
            var users = new List<UserDto>();

            foreach (var line in linesFromFile)
            {
                var newUser = new UserDto();
                var values = line.Split(',');

                newUser.Name = values[0];
                newUser.Email = values[1];
                newUser.Phone = values[2];
                newUser.Address = values[3];
                newUser.UserType = values[4];
                newUser.Money = decimal.Parse(values[5]);

                users.Add(newUser);
            }

            return users;
        }

        internal static bool IsUserExisting(UserDto userToCheck, List<UserDto> existingUsers)
        {
            if (existingUsers.Any(x => x.Email.ToLower() == userToCheck.Email || x.Phone == userToCheck.Phone))
                return true;

            if (existingUsers.Any(x => x.Name == userToCheck.Name && x.Address == userToCheck.Address))
                return true;

            return false;
        }

    }
}
