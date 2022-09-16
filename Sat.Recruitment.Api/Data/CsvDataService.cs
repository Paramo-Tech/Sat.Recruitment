using CsvHelper;
using CsvHelper.Configuration;
using Sat.Recruitment.Api.BusinessLogic.Model;
using Sat.Recruitment.Api.BusinessLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Sat.Recruitment.Api.Data
{
    public class CsvDataService : IDataService
    {
        private string _path;
        private CsvConfiguration _config;
        private List<User> _users;

        class UserMap : ClassMap<User>
        {
            public UserMap()
            {
                Map(p => p.Name).Index(0);
                Map(p => p.Email).Index(1);
                Map(p => p.Phone).Index(2);
                Map(p => p.Address).Index(3);
                Map(p => p.UserType).Index(4);
                Map(p => p.Money).Index(5);
            }
        }
        public CsvDataService(IConfiguration configuration)
        {
            this._path = Directory.GetCurrentDirectory() + configuration["database:path"];
            this._config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = Boolean.Parse(configuration["database:has_header"])
            };
            LoadUsers();
        }

        #region private
        private void LoadUsers()
        {
            using (var reader = new StreamReader(_path))
            using (var csv = new CsvReader(reader, _config))
            {
                csv.Context.RegisterClassMap<UserMap>();
                var records = csv.GetRecords<User>();
                this._users = records.ToList();
            }
        }

        private void SaveUsers()
        {
            using (var writer = new StreamWriter(_path))
            using (var csv = new CsvWriter(writer, _config))
            {
                csv.Context.RegisterClassMap<UserMap>();
                csv.WriteRecords<User>(this._users);
            }
        }
        #endregion

        public List<User> GetUsers()
        {
            return _users;
        }

        public void Save(User user)
        {
            this._users.Add(user);
            this.SaveUsers();
        }

        public bool Exists(Predicate<User> p)
        {
            return this._users.Exists(p);
        }

        public User GetUserBy(Predicate<User> p)
        {
            return this._users.Find(p);
        }
    }
}
