using Sat.Recruitment.Application.Dtos;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Sat.Recruitment.Domain;
using Microsoft.Extensions.Logging;


namespace Sat.Recruitment.Application.Users
{

    /// <summary>
    /// Command to create the user
    /// </summary>
    public class Create
    {
        public class command : IRequest
        {
            public CreateUserDto UserDto { get; set; }
        }

        public class Handler : IRequestHandler<command>
        {
            private readonly ILogger<Create> _logger;

            public Handler(ILogger<Create> logger)
            {
                _logger = logger;
            }

            public async Task<Unit> Handle(command request, CancellationToken cancellationToken)
            {
                var user = new User()
                {
                    Address = request.UserDto.Address
                };

                //TODO add data context to save in DB
                return Unit.Value;
            }
        }
    }
}
