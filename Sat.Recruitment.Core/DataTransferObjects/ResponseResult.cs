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
    public class ResponseResult<T> : IResponseResult<T>
    {
        public bool IsSuccess { get; set; }

        public T Result { get; set; }

        public IOperationDto? Operation { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="result"></param>
        public ResponseResult(bool isSuccess, T result, IOperationDto? operation = null)
        {
            IsSuccess = isSuccess;
            Result = result;
            Operation = operation;
        }

        /// <summary>
        /// Create a response of specific type
        /// </summary>
        /// <param name="success">true or false</param>
        /// <param name="response">Response</param>
        /// <returns></returns>
        public static ResponseResult<T> CreateResponse(bool success, T response, OperationDto? operation = null)
        {
            return new ResponseResult<T>(success, response, operation);
        }
    }
}
