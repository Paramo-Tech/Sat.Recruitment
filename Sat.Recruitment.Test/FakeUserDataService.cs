using Sat.Recruitment.Api.Features.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test
{
    public class FakeUserDataService : IUserDataService
    {
        public Task<IReadOnlyList<UserBase>> GetAll()
        {
            var users = (IReadOnlyList<UserBase>)new List<UserBase>
                {
                    new Normal("Juan", (Email)"Juan@marmol.com", "+5491154762312", "Peru 2464",1234M),
                    new Premium("Franco", (Email)"Franco.Perez@gmail.com", "+534645213542", "Alvear y Colombres,Premium",112234M),
                    new SuperUser("Agustina", (Email)"Agustina@gmail.com", "+534645213542", "Garay y Otra Calle",112234M),
                };

            return Task.FromResult(users);
        }
    }
}
