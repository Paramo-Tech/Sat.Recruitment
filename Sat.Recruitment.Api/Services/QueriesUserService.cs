using AutoMapper;
using Sat.Common;
using Sat.Recruitment.Api.Dtos;
using Sat.Recruitment.Api.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public interface IQueriesUserService
    {
        Task<IEnumerable<UserDto>> GetAllasync();
    }

    public class QueriesUserService : IQueriesUserService
    {
        private readonly IMapper mapper;

        public QueriesUserService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllasync()
        {
            var result = new List<UserDto>();
            using (var reader = FileRepository.ReadUsersFromFile())
            {
                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();
                    var user = new User
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = UserTypeString.GetByName(line.Split(',')[4].ToString()),
                        Money = line.Split(',')[5].ToString().ToDecimal(),
                    };
                    result.Add(mapper.Map<UserDto>(user));
                }
                reader.Close();

                return result;
            }
        }

        
    }
}
