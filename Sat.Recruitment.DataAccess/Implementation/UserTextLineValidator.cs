using System;
using Sat.Recruitment.DataAccess.Contracts;
using Sat.Recruitment.Domain;

namespace Sat.Recruitment.DataAccess.Implementation
{
    public class UserTextLineValidator : IUserTextLineValidator
    {
        private const char FieldSeparator = ',';
        private const int RequiredFields = 5;

        public (string[], bool) IsValid(string line)
        {
            if (string.IsNullOrEmpty(line))
                return (null, false);

            var fields = line.Split(FieldSeparator);

            if (fields.Length < RequiredFields)
                return (null, false);

            if (!Enum.TryParse(fields[4], out UserType _))
                return (null, false);

            return (fields, true);
        }
    }
}