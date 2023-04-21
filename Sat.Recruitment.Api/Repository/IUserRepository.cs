using Sat.Recruitment.Api.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModelDto>> Get();
        Task<UserModelDto> GetById(Guid userId);
        Task<UserModelDto> CreateUpdate(UserModelDto userDto);
        Task<bool> Delete(Guid userId);
        Task<bool> FindDuplicate(UserModelDto userDto);
    }
}
