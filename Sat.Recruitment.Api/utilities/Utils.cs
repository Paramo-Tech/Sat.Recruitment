using Sat.Recruitment.Api.Controllers;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;
using System.Net.Mail;

namespace Sat.Recruitment.Api.utilities
{
    public static class Utils
    {
        
        private static StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
        public static async Task<bool> IsDuplicatedUserAsync(User newUser)
        {
            var reader = ReadUsersFromFile();
            var duplicated = false;

            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var user = CreateUserFromCsv(line);

                if (user.Email == newUser.Email || user.Phone == newUser.Phone)
                {
                    duplicated = true;
                }
                else if (user.Name == newUser.Name && user.Address == newUser.Address)
                {
                    duplicated = true;
                    break;
                }
            }
            reader.Close();
            return duplicated;
        }

        public static User CreateUserFromCsv(string line)
        {
            var fields = line.Split(',');
            return new User
            {
                Name = fields[0],
                Email = fields[1],
                Phone = fields[2],
                Address = fields[3],
                UserType = fields[4],
                Money = decimal.Parse(fields[5])
            };
        }
        public static void ValidateErrors(string name, string email, string address, string phone, ref List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add("Name is required.");
                Console.WriteLine($"Error: Name is required.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add("Email is required.");
                Console.WriteLine($"Error: Email is required.");
            }
            else if (!IsValidEmail(email))
            {
                errors.Add("Invalid email format.");
                Console.WriteLine($"Error: Invalid email format.");
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                errors.Add("Address is required.");
                Console.WriteLine($"Error: Address is required.");
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                errors.Add("Phone is required.");
                Console.WriteLine($"Error: Phone is required.");
            }
            else if (!IsValidPhone(phone))
            {
                errors.Add("Invalid phone format.");
                Console.WriteLine($"Error: Invalid phone format.");
            }
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                var address = new MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPhone(string phone)
        {
            var regex = new Regex(@"^\d{10}$");
            return regex.IsMatch(phone);
        }

        public static string NormalizeEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email address cannot be null or empty.");
            }

            // Convert email address to lowercase
            email = email.ToLower();

            // Remove any leading or trailing whitespace
            email = email.Trim();

            // Remove any dots (.) before the @ symbol
            int index = email.IndexOf("@", StringComparison.Ordinal);
            if (index > 0)
            {
                email = email.Substring(0, index).Replace(".", "") + email.Substring(index);
            }

            // Remove any plus (+) and everything after it before the @ symbol
            index = email.IndexOf("+", StringComparison.Ordinal);
            if (index > 0)
            {
                email = email.Substring(0, index) + email.Substring(email.IndexOf("@", StringComparison.Ordinal));
            }

            // Remove any illegal characters
            email = Regex.Replace(email, @"[^a-z0-9@!#$%&'*+\/=?^_`{|}~-]", "", RegexOptions.IgnoreCase);

            return email;
        }
    }
}
