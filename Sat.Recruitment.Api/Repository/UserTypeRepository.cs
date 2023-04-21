using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Api.Contexts;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repository
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public UserTypeRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserTypeModelDto>> Get()
        {
            List<UserTypeModel> userTypeList = await _db.UsersTypes
                .ToListAsync();

            return _mapper.Map<List<UserTypeModelDto>>(userTypeList);
        }

        public async Task<string> GetTypeById(Guid typeId)
        {
            string typeName = await _db.UsersTypes.Where(x => x.UserTypeId == typeId)
                .Select(n => n.Name)
                .FirstOrDefaultAsync();

            return typeName;
        }
    }
}
