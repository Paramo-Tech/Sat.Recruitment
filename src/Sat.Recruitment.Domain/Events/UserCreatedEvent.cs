using Sat.Recruitment.Domain.Common;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Domain.Events
{
    public class UserCreatedEvent : BaseEvent
    {
        public UserCreatedEvent(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}
