using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.ApplicationLayer.Customers
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid CountryId { get; set; }
    }
}
