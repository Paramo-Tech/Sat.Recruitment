using System;

namespace Sat.Recruitment.Api
{
    public static class Constants
    {
        public static class UserTypes
        {
            public const string Normal = "Normal";
            public const string SuperUser = "SuperUser";
            public const string Premium = "Premium";
        }

        public static class UserTypesId
        {
            public static readonly Guid Normal = Guid.Parse("F0C909C0-6914-4939-9449-7DA80C2481FA");
            public static readonly Guid SuperUser = Guid.Parse("1BC8FFC9-3208-4F87-8D24-17D3C0D477BE");
            public static readonly Guid Premium = Guid.Parse("B5189434-5E6C-4185-816E-E38DA2A8F8BE");
        }

        public static class ValidationMessage
        {
            public const string DuplicateMessage = "The user is duplicated";
            public const string UserCreateMessage = "User Created Successfully";
            public const string UserDeleteMessage = "User Deleted Successfully";
        }
    }
}
