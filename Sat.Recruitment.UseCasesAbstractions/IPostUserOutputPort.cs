using Sat.Recruitment.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.UseCasesAbstractions
{
    public interface IPostUserOutputPort
    {
        Task Handle(Result result);
    }
}
