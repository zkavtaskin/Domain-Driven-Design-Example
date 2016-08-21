using eCommerce.Helpers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DomainModelLayer.Carts
{
    public class ProductAddedCart : DomainEvent
    {
        public CartProduct CartProduct { get; set; }

        public override void Flatten()
        {
            this.Args.Add("CartId", this.CartProduct.Cart.Id);
            this.Args.Add("CustomerId", this.CartProduct.Cart.Customer.Id);
            this.Args.Add("ProductId", this.CartProduct.Product.Id);
            this.Args.Add("Quantity", this.CartProduct.Quantity);
        }
    }
}
