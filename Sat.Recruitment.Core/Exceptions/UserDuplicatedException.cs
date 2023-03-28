using System.Runtime.Serialization;

namespace Sat.Recruitment.Core.Exceptions
{
    [Serializable]
    public class UserDuplicatedException : Exception
    {
        public UserDuplicatedException() : base()
        {
        }

        public UserDuplicatedException(string? message) : base(message)
        {
        }

        public UserDuplicatedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserDuplicatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
