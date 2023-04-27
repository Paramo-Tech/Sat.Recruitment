namespace Sat.Recruitment.Domain.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public decimal Money { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
