using Sat.Recruitment.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.UseCasesAbstractions
{
    public interface IPostUserInputPort
    {
        Task Handle(UserDTO user);
    }
}
