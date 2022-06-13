using Shared.Domain;

namespace Users.Domain.Specifications
{
    public class ExistingUserSpecification : ISpecification<User>
    {
        private readonly string name;
        private readonly Email email;
        private readonly string address;
        private readonly Phone phone;

        public ExistingUserSpecification(
            string name,
            Email email,
            string address,
            Phone phone)
        {
            this.name = name;
            this.email = email;
            this.address = address;
            this.phone = phone;
        }

        public bool IsSatisfied(User obj)
        {
            return obj.Email.Equals(this.email) ||
                obj.Phone.Equals(this.phone) ||
                (obj.Name.Equals(this.name) && obj.Address.Equals(this.address));
        }
    }
}
