using System;

namespace Sat.Recruitment.Core.Exceptions
{
    [Serializable]
    public class SequenceContainsMoreThanOneElementException
        : Exception
    {
        public SequenceContainsMoreThanOneElementException(string extraInformation = "")
            : base("The sequence contains more than one element" +
                   $"\r\nExtra information: {extraInformation}")
        {

        }
    }
}
