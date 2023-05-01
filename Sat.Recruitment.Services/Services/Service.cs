using Sat.Recruitment.Model.Shared;
using Sat.Recruitment.Model.User;
using Sat.Recruitment.Services.IServices;
using System.Reflection;

namespace Sat.Recruitment.Services.Services
{
    public class Service : IService
    {
        public async Task<ResponseModel> AddUser(UserModel user)
        {
            var content = string.Join(",",
                        typeof(UserModel)
                            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                            .Select
                            (
                                prop => prop.GetValue(user).ToString()
                            ));
            StreamWriter streamWriter = new StreamWriter("Files/Users.txt",true);
            await streamWriter.WriteLineAsync(content);
            streamWriter.Close();
            return new ResponseModel()
            {
                IsSuccess = true
            };
        }

        public async Task<List<UserModel>> GetUsers()
        {
            var users = new List<UserModel>();

            var lines = await File.ReadAllLinesAsync("Files/Users.txt");
            lines.ToList().ForEach(e => users.Add(new UserModel()
            {
                Name = e.Split(',')[0],
                Email = e.Split(',')[1],
                Phone = e.Split(',')[2],
                Address = e.Split(',')[3],
                UserType = (UserType)Enum.Parse(typeof(UserType), e.Split(',')[4]),
                Money = Convert.ToDecimal(e.Split(',')[5])
            }));
            return users;
        }

    }
}
