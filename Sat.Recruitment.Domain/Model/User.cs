using Sat.Recruitment.Domain.Enum;
using Sat.Recruitment.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Sat.Recruitment.Domain.Model
{
    public class User
    {
        public User()
        {
            
        }

        public User(
            string name,
            string email,
            string phone,
            string address,
            UserType type,
            decimal money)
        {
            this.Name = name;
            this.Address = address;
            this.Money = money;
            this.UserType = type;
            SetEmail(email);
            SetPhone(phone);
        }

        public long UserId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public UserType UserType { get; set; }

        public decimal Money { get; set; }

        public void SetPhone(string phoneNumber)
        {
            Regex validatePhoneNumberRegex = new Regex("^\\+?[1-9][0-9]{7,14}$");
            var isValid = validatePhoneNumberRegex.IsMatch(phoneNumber);

            if (isValid)
            {
                this.Phone = phoneNumber;
            }
            else
            {
                throw new DomainException("The phone number provided is invalid, it must match with the phone number pattern.");
            }
        }

        public void SetEmail(string emailString)
        {
            bool isValid = Regex.IsMatch(emailString, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            if (isValid)
            {
                this.Email = emailString;
            }
            else
            {
                throw new DomainException("The email address provided is invalid, it must match with the phone number pattern.");
            }
        }
    }
}
