using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sat.Recruitment.Core.Interfaces;

namespace Sat.Recruitment.Core.DataTransferObjects
{
    /// <summary>
    /// Class to define operation to return in http response
    /// </summary>
    public class OperationDto : IOperationDto
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public string Code { get; set; }

        public OperationDto()
        {
            Status = 0;
            Message = string.Empty;
            Code = string.Empty;
        }
    }
}
