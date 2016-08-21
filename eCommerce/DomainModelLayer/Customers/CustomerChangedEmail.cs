using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Domain;

namespace eCommerce.DomainModelLayer.Customers
{
    public class CustomerChangedEmail : DomainEvent
    {
        public Customer Customer { get; set; }

        public override void Flatten()
        {
            this.Args.Add("CustomerId", this.Customer.Id);
            this.Args.Add("Email", this.Customer.Email);
        }
    }
}
