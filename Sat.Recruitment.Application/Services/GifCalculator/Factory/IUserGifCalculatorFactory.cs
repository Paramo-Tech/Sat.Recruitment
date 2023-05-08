using Sat.Recruitment.Application.Services.GifCalculator.Strategy;
using Sat.Recruitment.Domain.Enum;

namespace Sat.Recruitment.Application.Services.GifCalculator.Factory
{
    /// <summary>
    /// Factory used to create UserGifCalculator according to the User's type.
    /// </summary>
    public interface IUserGifCalculatorFactory
    {
        /// <summary>
        /// Creates the calculator basing on the User's type.
        /// </summary>
        /// <param name="userType">User Type that will determine which strategy will be used for the calculator.</param>
        /// <returns>The instance of the calculator.</returns>
        IUserGifCalculator CreateCalculator(UserType userType);
    }
}
