using AutoMapper;
using MediatR;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Common.Models;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Application.Users.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMapper _mapper;
        public CreateUserHandler(IDbContext dbContext, IDateTimeProvider dateTimeProvider, IMapper mapper)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;
        }
        public Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (true)
            {

            }

            var newUser = _mapper.Map<User>(request);
        }
    }
}
