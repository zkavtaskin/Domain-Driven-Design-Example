using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.DomainModelLayer.Newsletter;
using eCommerce.DomainModelLayer.Customers;

namespace eCommerce.InfrastructureLayer
{
    public class WSNewsletterSubscriber : INewsletterSubscriber
    {
        public void Subscribe(Customer customer)
        {
            //call a third party web service here...
        }
    }
}
