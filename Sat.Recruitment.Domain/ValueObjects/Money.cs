using System;
namespace Sat.Recruitment.Domain.ValueObjects
{
	public class Money
	{
		public decimal Value { get; }
		public Money(decimal value)
		{
			this.Value = value;
		}
	}
}

