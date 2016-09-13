using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Domain;
using eCommerce.DomainModelLayer.Purchases;

namespace eCommerce.DomainModelLayer.Customers
{
    public class CustomerCheckedOut : DomainEvent
    {
        public Purchase Purchase { get; set; }

        public override void Flatten()
        {
            this.Args.Add("CustomerId", this.Purchase.CustomerId);
            this.Args.Add("PurchaseId", this.Purchase.Id);
            this.Args.Add("TotalCost", this.Purchase.TotalCost);
            this.Args.Add("TotalTax", this.Purchase.TotalTax);
            this.Args.Add("NumberOfProducts", this.Purchase.Products.Count);
        }
    }
}
