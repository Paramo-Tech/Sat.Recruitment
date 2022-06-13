using Users.Domain;
using Users.Domain.UserGif.Calculators;

namespace Users.UnitTest.Users.Domain.Specifications
{
    public static class DataProvider
    {
        public static IEnumerable<object[]> TestCases()
        {
            yield return new object[] { BaseUser(), BaseUser(), true };
            yield return new object[] { BaseUser(), OtherNameThanBase(), true };
            yield return new object[] { BaseUser(), OtherEmailThanBase(), true };
            yield return new object[] { BaseUser(), OnlySameEmailThanBase(), true };
            yield return new object[] { BaseUser(), OnlySamePhoneThanBase(), true };
            yield return new object[] { BaseUser(), SameNameAndAddressThanBase(), true };
            yield return new object[] { BaseUser(), DifferentThanBase(), false };
            yield return new object[] { BaseUser(), OnlySameNameThanBase(), false };
            yield return new object[] { BaseUser(), OnlySameAddressThanBase(), false };
        }

        private static User BaseUser() => new (
            "Mike",
            new("mike@gmail.com"),
            "Av. Juan G",
            new("+349 1122354215"),
            UserType.Normal,
            124
        );

        private static User DifferentThanBase() => new(
            "Cuba",
            new("cuba@gmail.com"),
            "Other street",
            new("+349 1122354216"),
            UserType.Normal,
            124
        );

        private static User OtherNameThanBase()
        {
            var originalUser = BaseUser();
            originalUser.Name = DifferentThanBase().Name;

            return originalUser;
        }

        private static User OtherEmailThanBase()
        {
            var originalUser = BaseUser();
            originalUser.Email = DifferentThanBase().Email;

            return originalUser;
        }

        private static User OnlySameEmailThanBase()
        {
            var differentUser = DifferentThanBase();
            differentUser.Email = BaseUser().Email;

            return differentUser;
        }

        private static User OnlySamePhoneThanBase()
        {
            var differentUser = DifferentThanBase();
            differentUser.Phone = BaseUser().Phone;

            return differentUser;
        }

        private static User SameNameAndAddressThanBase()
        {
            var differentUser = DifferentThanBase();
            differentUser.Name = BaseUser().Name;
            differentUser.Address = BaseUser().Address;

            return differentUser;
        }

        private static User OnlySameNameThanBase()
        {
            var differentUser = DifferentThanBase();
            differentUser.Name = BaseUser().Name;

            return differentUser;
        }

        private static User OnlySameAddressThanBase()
        {
            var differentUser = DifferentThanBase();
            differentUser.Address = BaseUser().Address;

            return differentUser;
        }
    }
}
