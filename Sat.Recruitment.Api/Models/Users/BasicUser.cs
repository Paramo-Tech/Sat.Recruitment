using Sat.Recruitment.Api.Models.Interfaces;

namespace Sat.Recruitment.Api.Models.Users
{
    public abstract class BasicUser : IUser
    {

        private string name;
        private string email;
        private string address;
        private string phone;
        private decimal money;
        private int id;

        public int Id { get => id; set => id= value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
        public decimal Money { get => money; set => money = value; }
        public abstract decimal Gift { get; }
    }
}
