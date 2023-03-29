namespace Sat.Recruitment.Api.Features.Users
{
    public abstract class UserBase
    {
        protected UserBase(string name, Email email, string address, string phone, decimal money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            Money = money + CalculateGift(money);
        }

        public string Name { get; protected set; }
        public Email Email { get; protected set; }
        public string Address { get; protected set; }
        public string Phone { get; protected set; }
        public decimal Money { get; protected set; }

        protected abstract decimal CalculateGift(decimal money);
    }

    public class Normal : UserBase
    {
        public Normal(string name, Email email, string address, string phone, decimal money)
            : base(name, email, address, phone, money) { }

        protected override decimal CalculateGift(decimal money)
        {
            var percentage = money > 100
                ? 0.12M
                : money > 10 ? 0.8M : 0M;
            return money * percentage;
        }
    }

    public class SuperUser : UserBase
    {
        public SuperUser(string name, Email email, string address, string phone, decimal money)
            : base(name, email, address, phone, money) { }

        protected override decimal CalculateGift(decimal money)
        {
            var percentage = money > 100 ? 0.20M : 0M;
            return money * percentage;
        }
    }

    public class Premium : UserBase
    {
        public Premium(string name, Email email, string address, string phone, decimal money)
            : base(name, email, address, phone, money) { }

        protected override decimal CalculateGift(decimal money)
        {
            var percentage = money > 100 ? 1M : 0;
            return money * percentage;
        }
    }
}
