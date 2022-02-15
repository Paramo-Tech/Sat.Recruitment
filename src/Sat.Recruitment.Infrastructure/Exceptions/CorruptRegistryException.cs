﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Infrastructure.Exceptions
{
    [Serializable]
    public class CorruptRegistryException
        : Exception
    {
        public CorruptRegistryException(string rowContent, string extraInformation = "")
            : base($"The row read did not have the appropriate number of columns (6), or some columns had inappropriate content." +
                   $"\r\nRow content: {rowContent}" +
                   $"\r\nExtra information: {extraInformation}")
        {
        }
    }
}
