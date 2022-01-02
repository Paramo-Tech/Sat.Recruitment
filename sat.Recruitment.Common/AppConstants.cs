using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Common
{
    public class AppConstants
    {
        public static class Validations
        {
            public static string NAME_REQ = "The name is required.";
            public static string ADDRESS_REQ = "The address is required.";
            public static string PHONE_REQ = "The phone is required.";
            public static string EMAIL_REQ = "The email is required.";
            public static string EMAIL_INV = "The email  is not valid.";
            public static string MONEY_AMOUNTT_INV = "The money amount is not valid.";
        }

        public static class Messages
        {
            public static string USER_DUPLICATED = "The user is duplicated.";
            public static string USER_CREATED = "User Created.";

        }
    }
}