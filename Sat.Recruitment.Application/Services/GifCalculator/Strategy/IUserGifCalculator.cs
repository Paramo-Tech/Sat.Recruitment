namespace Sat.Recruitment.Application.Services.GifCalculator.Strategy
{
    /// <summary>
    /// Calculator used to calculate user GIF.
    /// </summary>
    public interface IUserGifCalculator
    {
        /// <summary>
        /// Gets the total money for the user.
        /// </summary>
        /// <param name="money">Initial money from the user.</param>
        /// <returns>Total calculated money for the user.</returns>
        decimal Calculate(decimal money);
    }
}
