using System;

namespace Sat.Recruitment.Infrastructure.TextFile.Exceptions
{
    /// <summary>
    /// Exception used to handle situations where the storage (text file) is
    /// found to be corrupt for some reason (wrong formatted rows, repeated
    /// primary keys, etc.)
    /// </summary>
    [Serializable]
    public class CorruptStorageException
        : Exception
    {
        public CorruptStorageException(string extraInformation = "")
            : base("A problem was found in the data storage (text file)." +
                   $"\r\nExtra information: {extraInformation}")
        {

        }
    }
}
