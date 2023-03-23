using MediatR;
using Sat.Recruitment.Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Application.Queries.GetUser
{
    public class GetUserQuery : IRequest<User>
    {
        public int Id { get; set; }
    }
}
