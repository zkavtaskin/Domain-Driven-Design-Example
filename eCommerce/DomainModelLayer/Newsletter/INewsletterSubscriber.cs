using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.DomainModelLayer.Customers;

namespace eCommerce.DomainModelLayer.Newsletter
{
    public interface INewsletterSubscriber
    {
        void Subscribe(Customer customer);
    }
}
