using Shared.Domain;
using Shared.Domain.Exceptions;

namespace Users.Domain
{
    public class UserType : Enumeration
    {
        public static readonly UserType Normal = new ("Normal", "Normal user");
        public static readonly UserType SuperUser = new ("SuperUser", "Super user");
        public static readonly UserType Premium = new ("Premium", "Premium user");

        public static IEnumerable<UserType> List() => new[] { Normal, SuperUser, Premium };

        public static UserType FromDisplayName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.DisplayName, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new DomainException($"Possible values for UserType: {String.Join(",", List().Select(s => s.DisplayName))}");
            }

            return state;
        }

        public static UserType FromValue(string value)
        {
            var state = List().SingleOrDefault(s => s.Value == value);

            if (state == null)
            {
                throw new DomainException($"Possible values for UserType: {String.Join(",", List().Select(s => s.DisplayName))}");
            }

            return state;
        }

        private UserType(string value, string displayName) : base(value, displayName) { }
    }
}
