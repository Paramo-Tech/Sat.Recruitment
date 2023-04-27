using AutoMapper;
using MediatR;
using Sat.Recruitment.Application.Common.Interfaces;
using Sat.Recruitment.Application.Common.Models;
using Sat.Recruitment.Domain.Entities;
using System.Diagnostics;

namespace Sat.Recruitment.Application.Users.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IDbContext _dbContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMapper _mapper;
        private readonly IMoneyCalculatorService _moneyCalculatorService;
        private readonly IEmailHelper _emailHelper;
        public CreateUserHandler(IDbContext dbContext,
            IDateTimeProvider dateTimeProvider,
            IMapper mapper,
            IMoneyCalculatorService moneyCalculatorService,
            IEmailHelper emailHelper)
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;
            _moneyCalculatorService = moneyCalculatorService;
            _emailHelper = emailHelper;
        }
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //Validate existing user

            bool exists = _dbContext.Users.Any(m => m.Name == request.Name
            || m.Email == request.Email
            || m.Phone == request.Phone
            || m.Address == request.Address);

            if (exists)
                throw new ApplicationException("User is duplicated");

            var newUser = _mapper.Map<User>(request);

            newUser.CreatedDate = _dateTimeProvider.Now;
            newUser.Email = _emailHelper.NormalizeEmail(newUser.Email);

            _moneyCalculatorService.CalculateMoney(newUser);
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            Debug.WriteLine("User Created");

            return new Result()
            {
                IsSuccess = true,
                Message = "User Created"
            };
        }
    }
}
