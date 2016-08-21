using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.ApplicationLayer.History
{
    public class CustomerFriendlyEventDto
    {
        public string Email { get; set; }
        public DateTime When { get; set; }
        public string Action { get; set; }
    }
}
