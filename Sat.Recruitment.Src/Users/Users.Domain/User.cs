using Shared.Domain;

namespace Users.Domain
{
    public class User
    {
        public string Name { get; set; }
        public Email Email { get; set; }
        public string Address { get; set; }
        public Phone Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }
    }
}
