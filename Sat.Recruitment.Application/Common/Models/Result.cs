using System.Collections.Generic;
using System.Linq;

namespace CleanArchitecture.Application.Common.Models
{
    public class Result
    {
        internal Result(bool isSuccess, IEnumerable<string> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors.ToArray();
        }

        public bool IsSuccess { get; set; }

        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, new string[] { });
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }
}
