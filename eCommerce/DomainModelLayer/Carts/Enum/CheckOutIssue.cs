using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DomainModelLayer.Carts
{
    public enum CheckOutIssue
    {
        ProductNotInStock = 200,
        ProductIsFaulty = 201,
        UnpaidBalance = 100,
        NoActiveCreditCardAvailable = 101
    }
}
