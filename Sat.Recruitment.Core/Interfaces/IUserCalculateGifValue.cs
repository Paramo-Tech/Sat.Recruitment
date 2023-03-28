namespace Sat.Recruitment.Core.Interfaces
{
    /// <summary>
    /// Service definition to calculate gift value
    /// </summary>
    public interface IUserCalculateGiftValue
    {
        decimal GetGiftValue(string key, decimal money);
    }
}
