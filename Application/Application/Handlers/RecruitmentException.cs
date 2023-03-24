using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class RecruitmentException: Exception
    {
        public RecruitmentException(string? message) { 
            throw new Exception("GeneralError-"+message);
        }
    }
}
