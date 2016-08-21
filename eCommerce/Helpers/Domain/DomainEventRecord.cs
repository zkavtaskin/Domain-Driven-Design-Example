using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.Helpers.Domain
{
    public class DomainEventRecord
    {
        public string Type { get; set; }
        public DateTime Created { get; set; }
        public List<KeyValuePair<string, string>> Args { get; set; }
    }
}
