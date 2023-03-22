using System;
namespace Sat.Recruitment.Domain.Entities.UserAgregate.Rules
{
	public interface IGifStrategy
	{
        decimal CalculateGif(decimal money);
    }
}

