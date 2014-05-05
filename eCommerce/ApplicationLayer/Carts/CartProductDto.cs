using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.ApplicationLayer.Carts
{
    public class CartProductDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
