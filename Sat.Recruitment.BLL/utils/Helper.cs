using System;
using System.Collections.Generic;

namespace Sat.Recruitment.BLL.utils
{
    public static class Helper
    {
        public static decimal CalculateTotal(decimal money, decimal percentage)
        {
            decimal total = money;
            var gif = money * percentage;
            total = money + gif;
            return total;
        }
        public static decimal CalculateGif(string userType, decimal money)
        {
            decimal total = money;
            if (money > 100)
            {
                switch (userType)
                {
                    case UserType.NORMAL:
                        total = CalculateTotal(money, Convert.ToDecimal(UserType.NORMAL_PERC));
                        break;

                    case UserType.SUPERUSER:
                        total = CalculateTotal(money, Convert.ToDecimal(UserType.SUPERUSER_PERC));
                        break;

                    case UserType.PREMIUM:
                        total = CalculateTotal(money, UserType.PREMIUM_PERC);
                        break;

                }

            }
            else if (money > 10 && userType == UserType.NORMAL)
            {
                total = CalculateTotal(money, Convert.ToDecimal(UserType.NORMAL_PERC_LESS_100));
            }
            return total;
        }

        public static List<string> ValidateErrors(string name, string email, string address, string phone)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(name))
                //Validate if Name is null
                errors.Add($"The name is required");
            if (string.IsNullOrEmpty(email))
                //Validate if Email is null
                errors.Add("The email is required");
            if (string.IsNullOrEmpty(address))
                //Validate if Address is null
                errors.Add("The address is required");
            if (string.IsNullOrEmpty(phone))
                //Validate if Phone is null
                errors.Add("The phone is required");

            return errors;
        }

        public static string NormalizeEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be null or whitespace", nameof(email));
            }

            string[] parts = email.Split('@');
            if (parts.Length != 2)
            {
                throw new ArgumentException("Email is not in a valid format", nameof(email));
            }

            string localPart = parts[0].Replace(".", "");
            int plusIndex = localPart.IndexOf("+", StringComparison.Ordinal);
            if (plusIndex >= 0)
            {
                localPart = localPart.Remove(plusIndex, 1);
            }

            return $"{localPart}@{parts[1]}";
        }

    }
}
