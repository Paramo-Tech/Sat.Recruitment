using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sat.Recruitment.Domain.Common
{
    public abstract class BaseEntity
    {
        private readonly List<BaseEvent> _domainEvents = new List<BaseEvent>();

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }

    public abstract class BaseEntity<TKey> : BaseEntity
    {
        public TKey Id { get; set; }
    }

    public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey>
    {
        public DateTime CreatedUtc { get; set; }

        public string CreatedBy { get; set; }

        public DateTime LastModifiedUtc { get; set; }

        public string LastModifiedBy { get; set; }
    }
}
