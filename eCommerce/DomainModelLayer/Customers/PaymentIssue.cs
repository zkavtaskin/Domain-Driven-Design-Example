using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DomainModelLayer.Customers
{
    public enum PaymentIssues
    {
        UnpaidBalance = 100,
        NoActiveCreditCardAvailable = 101
    }
}
