using System;

namespace eCommerce.WebService.Models
{
    public class CustomerProductDto
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
}