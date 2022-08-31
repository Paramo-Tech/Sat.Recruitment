namespace Sat.Recruitment.Api.Utilities
{
    public interface IUtil
    {
        string NormaliceEmail(string email);
        decimal? MoneyTypeNormal(decimal? money);
        decimal? MoneyTypeSuperUser(decimal? money);
        decimal? MoneyTypePremium(decimal? money);
        string Md5_hash(string value);
        string TokenJWT(string email);
    }
}
