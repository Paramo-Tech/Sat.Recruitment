using Sat.Recruitment.Application.Common.Interfaces;

namespace Sat.Recruitment.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
