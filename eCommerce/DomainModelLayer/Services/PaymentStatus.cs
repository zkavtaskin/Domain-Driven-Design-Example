using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DomainModelLayer.Services
{
    public enum PaymentStatus
    {
        OK = 100,
        UnpaidBalance = 101,
        NoActiveCreditCardAvailable = 102
    }
}
