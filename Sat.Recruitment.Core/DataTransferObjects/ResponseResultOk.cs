using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sat.Recruitment.Core.Interfaces;

namespace Sat.Recruitment.Core.DataTransferObjects
{
    /// <summary>
    /// This class is used to response every call
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseResultOk<T> : IResponseResult<T>
    {
        public bool IsSuccess { get; set; }

        public T Result { get; set; }

        public IOperationDto? Operation { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="result"></param>
        public ResponseResultOk(T result, string? code = null)
        {
            IsSuccess = true;
            Result = result;
            Operation = new OperationDto {
                Code = code?? "200",
                Message = "Ok",
                Status = 200
            };
        }
    }
}
