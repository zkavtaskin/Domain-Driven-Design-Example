using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.ApplicationLayer.Products
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool Active { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public Guid ProductCodeId { get; set; }
        public string ProductCodeName { get; set; }
    }
}
