namespace Sat.Rec.Models
{
    public class UserType
    {
        public int UserTypeId { get; set; }
        public string Name { get; set; }

        public ICollection<GIFUserType> GIFUserType { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
