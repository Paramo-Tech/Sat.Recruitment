namespace Application.Contracts
{
    public interface IGiftFactory
    {
        IGiftService Create(string userType);
    }
}
