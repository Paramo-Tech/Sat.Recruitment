using System;
namespace Sat.Recruitment.Domain.ValueObjects
{
	public enum UserTypeEnum  {Normal, SuperUser, Premium };
	public class UserType: StringValueObject
	{
		public UserTypeEnum Type { get; }

		public UserType(string value):base(value)
		{
			EnsureIsUserType(value);
			Type = ConvertToEnum(value);
		}

        private UserTypeEnum ConvertToEnum(string value)
        {
            var result = UserTypeEnum.Normal;
            Enum.TryParse(value, out result);
            return result;
        }

        private bool EnsureIsUserType(string value)
        {
            var result = UserTypeEnum.Normal;
            if (Enum.TryParse(value, out result))
            {
                if (Enum.IsDefined(typeof(UserTypeEnum), result))
                {
                    return true;
                }
            }
            throw new ArgumentException($"The {nameof(UserType)} is required, current value {value}");//TODO: Custom exception
        }
    }
}

