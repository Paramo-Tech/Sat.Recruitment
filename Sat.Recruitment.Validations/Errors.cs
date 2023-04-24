namespace Sat.Recruitment.Validations
{
    public static class Errors
    {
        public const string NameIsRequired = "The name is required";
        public const string EmailIsRequired = "The email is required";
        public const string AddressIsRequired = "The address is required";
        public const string PhoneIsRequired = "The phone is required";
        public const string UserTypeIsRequired = "The user type is required";
        public const string UserTypeEnum = "The user type should be a valid value";
        public const string MoneyIsRequired = "The money is required";
        public const string MoneyGreaterThanZero = "The money must be a valid number";
    }
}