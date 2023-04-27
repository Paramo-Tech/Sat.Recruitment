using Sat.Recruitment.Application.Common.Interfaces;

namespace Sat.Recruitment.Infrastructure.Services
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
