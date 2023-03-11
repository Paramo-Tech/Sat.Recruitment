using Sat.Recruitment.Global.WebContracts;
using System;
using System.IO;

namespace Sat.Recruitment.Services
{
    public abstract class Helper
    {
        internal static string NormalizeEmail(User newUser)
        {
            //Normalize email
            var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            if (aux.Length < 2)
            {
                throw new AggregateException("Not valid Email");
            }

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            return string.Join("@", aux[0], aux[1]); ;
        }

        internal static StreamReader ReadUsersFromFile()
        {
            var path = $"{AppDomain.CurrentDomain.BaseDirectory}Files\\Users.txt";

            var fileStream = new FileStream(path, FileMode.Open);

            var reader = new StreamReader(fileStream);
            return reader;
        }
    }
}