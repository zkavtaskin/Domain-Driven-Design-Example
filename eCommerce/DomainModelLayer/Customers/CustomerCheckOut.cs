using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Domain;
using eCommerce.DomainModelLayer.Purchases;

namespace eCommerce.DomainModelLayer.Customers
{
    public class CustomerCheckedOut : IDomainEvent
    {
        public Purchase Purchase { get; set; }
    }
}
