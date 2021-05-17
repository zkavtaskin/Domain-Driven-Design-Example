﻿using eCommerce.Helpers.Domain;

namespace eCommerce.DomainModelLayer.Carts
{
    public class ProductRemovedCart : DomainEvent
    { 
        public CartProduct CartProduct { get; set; }

        public override void Flatten()
        {
            this.Args.Add("CartId", this.CartProduct.CartId);
            this.Args.Add("CustomerId", this.CartProduct.CustomerId);
            this.Args.Add("ProductId", this.CartProduct.ProductId);
            this.Args.Add("Quantity", this.CartProduct.Quantity);
        }
    }
}
