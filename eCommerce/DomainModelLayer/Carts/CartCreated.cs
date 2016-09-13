using eCommerce.Helpers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DomainModelLayer.Carts
{
    public class CartCreated : DomainEvent
    {
        public Cart Cart { get; set; }

        public override void Flatten()
        {
            this.Args.Add("CustomerId", this.Cart.CustomerId);
            this.Args.Add("CartId", this.Cart.Id);
        }
    }
}
