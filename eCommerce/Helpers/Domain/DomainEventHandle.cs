using eCommerce.Helpers.Logging;
using eCommerce.Helpers.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.Helpers.Domain
{
    public class DomainEventHandle<TDomainEvent> : Handles<TDomainEvent>
        where TDomainEvent : DomainEvent
    {
        IDomainEventRepository domainEventRepository;
        IRequestCorrelationIdentifier requestCorrelationIdentifier;

        public DomainEventHandle(IDomainEventRepository domainEventRepository, 
            IRequestCorrelationIdentifier requestCorrelationIdentifier)
        {
            this.domainEventRepository = domainEventRepository;
            this.requestCorrelationIdentifier = requestCorrelationIdentifier;
        }

        public void Handle(TDomainEvent @event)
        {
            @event.Flatten();
            @event.CorrelationID = this.requestCorrelationIdentifier.CorrelationID;
            this.domainEventRepository.Add(@event);
        }
    }
}
