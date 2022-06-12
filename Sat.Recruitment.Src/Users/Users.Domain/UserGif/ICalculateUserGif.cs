namespace Users.Domain.UserGif
{
    public interface ICalculateUserGif
    {
        decimal Execute(UserType userType, decimal currentMoney);
    }
}
