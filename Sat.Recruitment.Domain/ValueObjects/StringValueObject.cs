using System;
namespace Sat.Recruitment.Domain.ValueObjects
{
    public abstract class StringValueObject : ValueObject
    {
        public string Value { get; }

        public StringValueObject(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Value;
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

            if (obj is not StringValueObject item)
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

