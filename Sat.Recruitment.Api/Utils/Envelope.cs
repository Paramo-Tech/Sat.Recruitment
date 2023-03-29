using System;

namespace Sat.Recruitment.Api.Utils
{
    public class Envelope<T>
    {
        public T Result { get; }
        public string ErrorMessage { get; }
        public bool HasError => ErrorMessage != null;

        protected internal Envelope(T result, string errorMessage)
        {
            Result = result;
            ErrorMessage = errorMessage;
        }
    }

    public class Envelope : Envelope<string>
    {
        protected Envelope(string errorMessage) : base(null, errorMessage) { }

        public static Envelope<T> Ok<T>(T result) => new Envelope<T>(result, null);

        public static Envelope Ok() => new Envelope(null);

        public static Envelope Error(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage)) throw new ArgumentNullException(nameof(errorMessage));

            return new Envelope(errorMessage);
        }
    }
}
