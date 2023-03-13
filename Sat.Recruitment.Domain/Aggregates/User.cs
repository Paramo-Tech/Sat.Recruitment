using System;
using Sat.Recruitment.Domain.ValueObjects;

namespace Sat.Recruitment.Domain.Aggregates
{
	public class User : AggregateRoot
	{
        public UserName Name { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public Phone Phone { get; private set; }
        public UserType Type { get; private set; }
        public Money Money { get; private set; }
        public User(UserName name, Email email, Address address, Phone phone, UserType type, Money value)
		{
            this.Address = address;
            this.Email = email;
            this.Money = value;
            this.Name = name;
            this.Type = type;
            this.Phone = phone;
		}

        public static User Create(UserName name, Email email, Address address, Phone phone, UserType type, Money money)
        {
            money = CalculateGift(type, money);

            var user = new User(name, email, address, phone, type, money);
            return user;
        }

        private static Money CalculateGift(UserType type, Money money)
        {
            decimal percentage;
            decimal gif;

            switch (type.Type)
            {
                case UserTypeEnum.Normal:
                    if (money.Value > 100)
                    {
                        percentage = 0.12m;
                        gif = money.Value * percentage;
                        money = new Money(money.Value + gif);
                    }
                    else if (money.Value > 10)
                    {
                        percentage = 0.8m;
                        gif = money.Value * percentage;
                        money = new Money(money.Value + gif);
                    }
                    break;
                case UserTypeEnum.SuperUser:
                    if (money.Value > 100)
                    {
                        percentage = 0.20m;
                        gif = money.Value * percentage;
                        money = new Money(money.Value + gif);
                    }
                    break;
                case UserTypeEnum.Premium:
                    if (money.Value > 100)
                    {
                        gif = money.Value * 2;
                        money = new Money(money.Value + gif);
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid user type.");
            }

            return money;
        }

        //public static User Create(UserName name, Email email, Address address, Phone phone, UserType type, Money money)
        //{
        //    var newValue = money;
        //    if (type.Type == UserTypeEnum.Normal)
        //    {
        //        if (money.Value > 100)
        //        {
        //            var percentage = Convert.ToDecimal(0.12);
        //            //If new user is normal and has more than USD100
        //            var gif = money.Value * percentage;
        //            newValue = new Money(money.Value + gif);
        //        }
        //        if (money.Value < 100)
        //        {
        //            if (money.Value > 10)
        //            {
        //                var percentage = Convert.ToDecimal(0.8);
        //                var gif = money.Value * percentage;
        //                newValue = new Money(money.Value + gif);
        //            }
        //        }
        //    }
        //    if (type.Type == UserTypeEnum.SuperUser)
        //    {
        //        if (money.Value > 100)
        //        {
        //            var percentage = Convert.ToDecimal(0.20);
        //            var gif = money.Value * percentage;
        //            newValue = new Money(money.Value + gif);
        //        }
        //    }
        //    if (type.Type == UserTypeEnum.Premium)
        //    {
        //        if (money.Value > 100)
        //        {
        //            var gif = money.Value * 2;
        //            newValue = new Money(money.Value + gif);
        //        }
        //    }

        //    var user = new User(name, email, address, phone, type, newValue);
        //    return user;
        //}
    }
}

