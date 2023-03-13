using System;
namespace Sat.Recruitment.Domain.Events
{
    public abstract class DomainEvent
    {
        public string AggregateId { get; } = null!;
        public string EventId { get; } = null!;
        public string OccurredOn { get; } = null!;

        //protected DomainEvent(string aggregateId, string? eventId, string? occurredOn)
        //{
        //    this.AggregateId = aggregateId;
        //    this.EventId = eventId ?? UuidValueObject.Random().Value;
        //    this.OccurredOn = occurredOn ?? DateUtils.DateToString(DateTime.Now);
        //}

        //protected DomainEvent()
        //{
        //}

        //public abstract string EventName();
        //public abstract Dictionary<string, string> ToPrimitives();

        //public abstract DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId,
        //    string occurredOn);
    }
}

