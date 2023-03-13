using System;
namespace Sat.Recruitment.Domain.ValueObjects
{
	public class UserName: StringValueObject
	{
		public UserName(string name) :base(name)
		{
			EnsureNotEmpy(name);
		}

        private void EnsureNotEmpy(string name)
        {
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException($"The {nameof(UserName)} is required, current value {name}");//TODO: Custom exception
			}
        }
    }
}

