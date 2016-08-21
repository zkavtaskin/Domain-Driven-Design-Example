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

        public DomainEventHandle(IDomainEventRepository domainEventRepository)
        {
            this.domainEventRepository = domainEventRepository;
        }

        public void Handle(TDomainEvent args)
        {
            args.Flatten();
            this.domainEventRepository.Add(args);
        }
    }
}
