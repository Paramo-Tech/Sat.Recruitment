using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Sat.Recruitment.Api.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class DomainExceptionMapperAttribute : Attribute, IFilterMetadata
    {
        public string ExceptionTypeName { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}

