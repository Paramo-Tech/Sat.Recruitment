using MediatR;
using Sat.Recruitment.Application.Users.UserTypeStrategy;

namespace Sat.Recruitment.Application.Users.Commands
{
    public record CreateUserCommand : IRequest<int>
    {
        private decimal _money;
        private string? _userType;
        private IUserTypeStrategy UserTypeStrategy { get; set; } = new DefaultUserTypeStrategy();
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? UserType
        {
            get { return _userType!; }
            set
            {
                _userType = value;

                UserTypeStrategy = _userType switch
                {
                    "Normal" => new NormalUserTypeStrategy(),
                    "SuperUser" => new SuperUserTypeStrategy(),
                    "Premium" => new PremiumUserTypeStrategy(),
                    _ => new DefaultUserTypeStrategy(),
                };
            }
        }

        public decimal Money
        {
            get => UserTypeStrategy!.CalculateGif(_money);
            set { _money = value; }
        }
    }
}
