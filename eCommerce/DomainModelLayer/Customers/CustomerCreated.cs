using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Domain;

namespace eCommerce.DomainModelLayer.Customers
{
    public class CustomerCreated : DomainEvent
    {
        public Customer Customer { get; set; }

        public override void Flatten()
        {
            this.Args.Add("FirstName", this.Customer.FirstName);
            this.Args.Add("LastName", this.Customer.LastName);
            this.Args.Add("Email", this.Customer.Email);
            this.Args.Add("Country", this.Customer.CountryId);
        }
    }
}
