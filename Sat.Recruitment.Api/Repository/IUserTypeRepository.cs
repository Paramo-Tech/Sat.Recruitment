using Sat.Recruitment.Api.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repository
{
    public interface IUserTypeRepository
    {
        Task<string> GetTypeById(Guid typeId);
        Task<IEnumerable<UserTypeModelDto>> Get();

    }
}
