using eCommerce.Helpers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DomainModelLayer.Products
{
    public class ProductCreated : DomainEvent
    {
        public Product Product { get; set; }

        public override void Flatten()
        {
            this.Args.Add("ProductId", this.Product.Id);
            this.Args.Add("ProductName", this.Product.Name);
            this.Args.Add("ProductQuantity", this.Product.Quantity);
            this.Args.Add("ProductCode", this.Product.Code.Id);
            this.Args.Add("ProductCost", this.Product.Cost);
        }
    }
}
