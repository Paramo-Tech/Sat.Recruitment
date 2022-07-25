using Microsoft.AspNetCore.DataProtection;
using Sat.Recruitment.BusinessLogic.ExternalServices;

namespace Sat.Recruitment.DataAccess.Helpers
{
    public class EncryptDecrypt : IEncryptDecrypt
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private const string Key = "knzKHdSuKcuCUP35tuCkykCt";

        public EncryptDecrypt(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public string Encrypt(string input)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Protect(input);
        }

        public string Decrypt(string cipherText)
        {
            var protector = _dataProtectionProvider.CreateProtector(Key);
            return protector.Unprotect(cipherText);
        }
    }
}