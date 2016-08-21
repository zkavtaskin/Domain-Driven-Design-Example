using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.ApplicationLayer.History
{
    public class CustomerFriendlyHistoryDto
    {
        public List<CustomerFriendlyEventDto> Events { get; set; } = new List<CustomerFriendlyEventDto>();
    }
}
