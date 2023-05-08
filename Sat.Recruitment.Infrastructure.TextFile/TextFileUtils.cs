using Sat.Recruitment.Domain.Model;
using System;

namespace Sat.Recruitment.Infrastructure.TextFile
{
    public static class TextFileUtils
    {
        public static string GetElementFromLine(int position, string line)
        {
            try
            { 
                return line.Split(',')[position].ToString();
            }
            catch
            {
                throw new InvalidOperationException();
            }
        }

        public static string UserToFileLine(User user)
        {
            return $"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{user.Money}";
        }
    }
}
