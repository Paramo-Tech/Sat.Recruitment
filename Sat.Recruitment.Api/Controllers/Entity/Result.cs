using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers.Entity
{
    public class Result
    {
        public Result(bool isSuccess, string message)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
        } 

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
