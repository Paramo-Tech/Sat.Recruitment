using System;

namespace Sat.Recruitment.Core.Exceptions
{
    [Serializable]
    public class EntityNotFoundException
        : Exception
    {
        public EntityNotFoundException(string entityTypeName, string extraInformation = "")
            : base($"The entity {entityTypeName} was not found." +
                   $"\r\nExtra information: {extraInformation}")
        {

        }
    }
}
