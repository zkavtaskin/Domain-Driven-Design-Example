using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.ApplicationLayer.Carts
{
    public class CartDto
    {
        public Guid CustomerId { get; set; }
        public List<CartProductDto> Products { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
