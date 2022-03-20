using System.Runtime.Serialization;

namespace Sat.Recruitment.Api.Constants
{
    public static class Messages
    {
        public const string NameErrorRequired = "The name is required";

        public const string EmailErrorRequired = "The email is required";

        public const string AddressErrorRequired = "The address is required";

        public const string PhoneErrorRequired = "The phone is required";

        public const string PasswordErrorRequired = "The password is required";

        public const string GenericError = "An error has occured. Please check the log for more details";

        public const string WrongUserPassowrd = "Wrong user or password";

        public const string DuplicationError = "The user is duplicated";

        public const string DuplicateKey = "duplicate key";

        public const string DeleteError = "There is no User with the provided identifier";

        public const string UniqueError = "UNIQUE constraint failed";
    }
}
