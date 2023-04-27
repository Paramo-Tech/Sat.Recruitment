namespace Sat.Recruitment.Api.Models
{
    /// <summary>
    /// Object returned by the API
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Returns true if the call was successfully completed
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Contains the errors that may raise in the API call
        /// </summary>
        public string Errors { get; set; }
    }
}
