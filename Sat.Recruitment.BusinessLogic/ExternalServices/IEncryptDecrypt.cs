namespace Sat.Recruitment.BusinessLogic.ExternalServices
{
    public interface IEncryptDecrypt
    {
        string Encrypt(string input);
        string Decrypt(string input);
    }
}