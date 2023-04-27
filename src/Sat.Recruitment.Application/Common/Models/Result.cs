using System;
using System.Collections.Generic;
using System.Linq;

namespace Sat.Recruitment.Application.Common.Models
{
    /// <summary>
    /// Object returned by the API
    /// </summary>
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        /// <summary>
        /// Returns true when the API call was successfully completed
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Has a list of all the errors raised in the API call
        /// </summary>
        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, Array.Empty<string>());
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }
}
