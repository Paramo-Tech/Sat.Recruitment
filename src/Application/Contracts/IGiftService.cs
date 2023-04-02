namespace Application.Contracts
{
    public interface IGiftService
    {
        string Type { get; set; }
        decimal GetDiscount(decimal money);
    }
}