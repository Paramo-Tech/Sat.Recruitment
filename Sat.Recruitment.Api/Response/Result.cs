using System;

namespace Sat.Recruitment.Api.Response
{
    /// <summary>
    /// This is used to create API response
    /// </summary>
    [Serializable]
    public class Result<T>
    {
        /// <summary>
        /// Defines if response is success or not.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// If IsSuccess is false returns errors, else returns information message.
        /// </summary>
        public T Response { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="success"></param>
        /// <param name="response"></param>
        public  Result(bool success, T response)
        {
            IsSuccess = success;
            Response = response;
        }

        /// <summary>
        /// Create a response of specific type
        /// </summary>
        /// <param name="success">true or false</param>
        /// <param name="response">Response</param>
        /// <returns></returns>
        public static Result<T> CreateResponse(bool success, T response)
        {
            return new Result<T>(success, response);
        }
    }
}
