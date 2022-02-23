using System;

namespace Sat.Recruitment.Core.Exceptions
{
    [Serializable]
    public class EntityAlreadyExistsException
        : Exception
    {
        public EntityAlreadyExistsException(string entityTypeName, string extraInformation = "")
            : base($"The entity {entityTypeName} already exists." +
                   $"\r\nExtra information: {extraInformation}")
        {

        }
    }
}
