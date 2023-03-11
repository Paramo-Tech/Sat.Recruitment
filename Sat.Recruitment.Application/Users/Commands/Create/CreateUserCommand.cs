using CleanArchitecture.Application.Common.Models;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Dto;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Users.Commands.Create
{
    public record CreateUserCommand : IRequestWrapper<UserDto>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal Money { get; set; }
        public string UserType { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandlerWrapper<CreateUserCommand, UserDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                Name = request.Name,
                Email = NormalizeEmail(request.Email),
                Address = request.Address,
                Phone = request.Phone,
                UserType = _context.UserTypes.FirstOrDefault(x => x.Name == request.UserType),
                Money = request.Money

            };

            await _context.Users.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<UserDto>(entity));
        }

        private string NormalizeEmail(string email)
        { 
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            return string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}
