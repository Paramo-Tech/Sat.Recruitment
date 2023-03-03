using System;
using System.Text.RegularExpressions;

namespace Core.Entities
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }

        public void NormalizeEmail()
        {
            // check if the email is already normalized
            if (Regex.IsMatch(this.Email, @"^[a-z0-9](\.?[a-z0-9]){0,}@([a-z0-9]+\.)+[a-z]{2,}$", RegexOptions.IgnoreCase))
            {
                return;
            }

            // Normalize the email
            var atIndex = this.Email.IndexOf("+", StringComparison.Ordinal);
            var username = atIndex < 0 ? this.Email.Split('@')[0].Replace(".", "") : this.Email.Split('@')[0].Replace(".", "").Remove(atIndex);
            var domain = this.Email.Split('@')[1];
            this.Email = $"{username}@{domain}";
        }
    }

    public class NormalUser : User
    {
        public NormalUser(string money)
        {
            if (decimal.Parse(money) > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                //If new user is normal and has more than USD100
                var gif = decimal.Parse(money) * percentage;
                this.Money = this.Money + gif;
            }
            if (decimal.Parse(money) < 100)
            {
                if (decimal.Parse(money) > 10)
                {
                    var percentage = Convert.ToDecimal(0.8);
                    var gif = decimal.Parse(money) * percentage;
                    this.Money = this.Money + gif;
                }
            }
        }
    }

    public class SuperUser : User
    {
        public SuperUser(string money)
        {
            if (decimal.Parse(money) > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gif = decimal.Parse(money) * percentage;
                this.Money = this.Money + gif;
            }
        }
    }

    public class PremiumUser : User
    {
        public PremiumUser(string money)
        {
            if (decimal.Parse(money) > 100)
            {
                var gif = decimal.Parse(money) * 2;
                this.Money = this.Money + gif;
            }
        }
    }

    public enum UserTypes
    {
        Normal,
        SuperUser,
        Premium
    }
}
