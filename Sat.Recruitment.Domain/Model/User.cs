namespace Sat.Recruitment.Domain.Model
{
    public class User
    {

        public int Id { get; set; }

        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Address { get; set; }
        
        public string Phone { get; set; }

        public string UserType { get; set; }

        public decimal Money { get; set; }

    }
}
