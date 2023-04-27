using System;
using Sat.Recruitment.Application.Common.Interfaces;

namespace Sat.Recruitment.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }

}
