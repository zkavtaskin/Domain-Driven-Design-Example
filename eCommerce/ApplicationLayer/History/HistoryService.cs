using eCommerce.Helpers.Domain;
using eCommerce.Helpers.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.ApplicationLayer.History
{
    public class HistoryService : IHistoryService
    {
        IDomainEventRepository domainEventRepository;

        public HistoryService(IDomainEventRepository domainEventRepository)
        {
            this.domainEventRepository = domainEventRepository;
        }

        public HistoryDto GetHistory()
        {
            IEnumerable<DomainEventRecord> events = this.domainEventRepository.FindAll();

            HistoryDto history = new HistoryDto();
            history.Events = AutoMapper.Mapper.Map<IEnumerable<DomainEventRecord>, List<EventDto>>(events);

            return history;
        }
    }
}
