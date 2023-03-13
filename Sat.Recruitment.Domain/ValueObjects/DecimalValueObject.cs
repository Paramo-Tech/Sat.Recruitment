using System;
using System.Globalization;

namespace Sat.Recruitment.Domain.ValueObjects
{
	public class DecimalValueObject: ValueObject
	{

        public decimal Value { get; }

        public DecimalValueObject(decimal value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString(NumberFormatInfo.InvariantInfo);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Value;
        }

        public override bool Equals(object? obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj is not DecimalValueObject item)
            {
                return false;
            }

            return this.Value == item.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Value);
        }
    }

    
}

