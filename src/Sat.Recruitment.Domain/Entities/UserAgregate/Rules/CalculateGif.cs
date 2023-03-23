using System;
namespace Sat.Recruitment.Domain.Entities.UserAgregate.Rules
{
	public static class CalculateGif
	{
		public static IGifStrategy CreateGif(string userType)
		{
            return userType switch
            {
                "Normal" => new GifNormal(),
                "Premium" => new GifPremium(),
                "SuperUser" => new GifSuperUser(),
                _ => new GifNormal(),
            };
        }
	}
}

