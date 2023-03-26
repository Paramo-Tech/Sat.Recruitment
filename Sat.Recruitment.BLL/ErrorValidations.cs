using System;
using System.Text;
using Sat.Recruitment.BO;

namespace Sat.Recruitment.BLL
{
    public class ErrorValidations
    {
        private static Response.ResponseList[] list = new Response.ResponseList[2];

        public static string BuildErrorMessage(string name, string email, string address, string phone)
        {
            StringBuilder errors = new StringBuilder();

            if (name == null)
            {
                errors.Append("The name is required. ");
            }
            if (email == null)
            {
                errors.Append("The email is required. ");
            }
            if (address == null)
            {
                errors.Append("The address is required. ");
            }
            if (phone == null)
            {
                errors.Append("The phone is required. ");
            }

            if (errors.Length > 0)
            {
                throw new Exception(errors.ToString());
            }

            return "";
        }

        public static string BuildErrorMessage(string type)
        {
            StringBuilder errors = new StringBuilder();

            switch (type)
            {
                case "email":
                    errors.Append("Invalid email format.");
                    break;
            }

            if (errors.Length > 0)
            {
                throw new Exception(errors.ToString());
            }

            return "";
        }

        public static string BuildErrorMessage(int Id)
        {
            list[0] = new Response.ResponseList { Id = 0, Error = "The user is duplicated" };

            if (Id > -1)
                return list[Id].Error;

            return "";
        }
    }
}
