using Sat.Recruitment.BusinessLogic.ExternalServices;
using Sat.Recruitment.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sat.Recruitment.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly IAppConfiguration _appConfiguration;
        private readonly IEncryptDecrypt _encryptDecrypt;
        private readonly string _filePath;

        public DataAccess(IAppConfiguration appConfiguration, IEncryptDecrypt encryptDecrypt)
        {
            _appConfiguration = appConfiguration;
            _encryptDecrypt = encryptDecrypt;
            _filePath = string.Concat(Directory.GetCurrentDirectory(), _appConfiguration.FilePath);
        }

        public async Task<List<User>> ReadData()
        {
            try
            {
                var jsonData = await File.ReadAllTextAsync(_filePath);
                if (string.IsNullOrWhiteSpace(jsonData))
                    return new List<User>();

                return JsonSerializer.Deserialize<List<User>>(_encryptDecrypt.Decrypt(jsonData));
            }
            catch
            {
                return new List<User>();
            }
        }

        public async Task<bool> SaveData(List<User> objList)
        {
            try
            {
                string newJson = JsonSerializer.Serialize(objList);
                File.WriteAllText(_filePath, _encryptDecrypt.Encrypt(newJson));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}