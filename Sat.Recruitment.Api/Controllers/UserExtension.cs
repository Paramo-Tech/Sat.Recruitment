using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Sat.Recruitment.Api.Controllers
{
    public class UsersExtension
    {
        public decimal calculateUserMoneyPercentaje(UserType userType, decimal userMoney)
        {
            try
            {
                decimal percentage = 0;
                switch (userType)
                {
                    case UserType.Normal:
                        //If new user is normal and has more than USD100
                        percentage = userMoney > 100 ? Convert.ToDecimal(0.12) : Convert.ToDecimal(0.8);
                        break;
                    case UserType.SuperUser:
                        if (userMoney > 100)
                            percentage = Convert.ToDecimal(0.20);
                        break;
                    case UserType.Premium:
                        if (userMoney > 100)
                            percentage = 2;
                        break;
                    default:
                        break;
                }
                decimal gif = userMoney * percentage;
                return userMoney + gif;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string normalizeUserEmail(string userEmail)
        {
            try
            {
                var aux = userEmail.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
                aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
                return string.Join("@", new string[] { aux[0], aux[1] });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
