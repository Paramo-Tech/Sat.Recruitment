using System;
namespace Sat.Recruitment.Domain.ValueObjects
{
	public class Money
	{
		public decimal Value { get; }

		public Money(string value)
		{
			EnsureIsDecimal(value);
			this.Value = decimal.Parse(value);
		}

        public Money(decimal value)
        {
            this.Value = value;
        }

        private void EnsureIsDecimal(string value)
        {
			decimal result = 0m;
			if (!decimal.TryParse(value, out result))
			{
                throw new ArgumentException($"The {nameof(Money)} is required, current value {value}");//TODO: Custom exception
            }
        }
    }
}

