using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Features.Users
{
    public interface IUserDataService
    {
        Task<IReadOnlyList<UserBase>> GetAll();
    }

    public class UserDataService : IUserDataService
    {
        public async Task<IReadOnlyList<UserBase>> GetAll()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            var lines = await File.ReadAllLinesAsync(path);
            var users = new List<UserBase>();

            foreach (var line in lines)
            {
                var data = line.Split(',');
                var name = data[0];
                var email = Email.Create(data[1]);
                var phone = data[2];
                var address = data[3];
                var type = data[4];
                var money = decimal.Parse(data[5]);

                var user = (UserBase)Activator.CreateInstance(MapUserTypeToClass(type),
                    name, email, address, phone, money);

                users.Add(user);
            }

            return users;
        }

        private Type MapUserTypeToClass(string userType)
        {
            var userBaseType = typeof(UserBase);

            var derivedTypeName = userBaseType.FullName.Replace(nameof(UserBase), userType);
            var assemblyName = userBaseType.GetTypeInfo().Assembly.FullName;
            var fullQualifiedName = $"{derivedTypeName}, {assemblyName}";
            var derivedType = Type.GetType(fullQualifiedName);

            return derivedType;
        }
    }
}
