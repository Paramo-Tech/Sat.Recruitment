using System;

namespace Sat.Recruitment.Model
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal OriginalMoney
        {
            get; set;
        }
        public decimal Money
        {
            get
            {
                decimal money = OriginalMoney;
                decimal gifPercentage = 1;
                if (UserType == "Normal")
                {
                    if (OriginalMoney > 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        //If new user is normal and has more than USD100
                        gifPercentage += percentage;
                    }
                    if (OriginalMoney < 100)
                    {
                        if (OriginalMoney > 10)
                        {
                            var percentage = Convert.ToDecimal(0.8);
                            gifPercentage += percentage;
                        }
                    }
                }
                if (UserType == "SuperUser")
                {
                    if (OriginalMoney > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        gifPercentage += percentage;
                    }
                }
                if (UserType == "Premium")
                {
                    if (OriginalMoney > 100)
                    {
                        gifPercentage += 2;
                    }
                }
                return money * gifPercentage;
            }
        }
    }
}