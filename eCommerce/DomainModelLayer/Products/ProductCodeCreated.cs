using eCommerce.Helpers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DomainModelLayer.Products
{
    public class ProductCodeCreated : DomainEvent
    {
        public ProductCode ProductCode { get; set; }

        public override void Flatten()
        {
            this.Args.Add("ProductCodeId", this.ProductCode.Id);
            this.Args.Add("ProductCodeName", this.ProductCode.Name);
        }
    }
}
