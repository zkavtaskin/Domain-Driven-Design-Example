using eCommerce.Helpers.Domain;
using eCommerce.Helpers.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.InfrastructureLayer
{
    public class MemDomainEventRepository : IDomainEventRepository
    {
        private List<DomainEventRecord> domainEvents = new List<DomainEventRecord>();

        public void Add<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent
        {
            this.domainEvents.Add(
                new DomainEventRecord()
                {
                    Created = domainEvent.Created,
                    Type = domainEvent.Type,
                    Args = domainEvent.Args.Select(kv => new KeyValuePair<string, string>(kv.Key, kv.Value.ToString())).ToList(),
                    CorrelationID = domainEvent.CorrelationID
                });
        }

        public IEnumerable<DomainEventRecord> FindAll()
        {
            return this.domainEvents;
        }
    }
}
