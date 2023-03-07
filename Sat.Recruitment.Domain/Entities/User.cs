namespace Sat.Recruitment.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set; }
        public string? UserType { get; set; }
        public decimal Money { get; set; }
    }
}
