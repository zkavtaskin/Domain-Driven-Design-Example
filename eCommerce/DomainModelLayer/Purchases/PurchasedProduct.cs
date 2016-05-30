using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.DomainModelLayer.Carts;
using eCommerce.DomainModelLayer.Products;

namespace eCommerce.DomainModelLayer.Purchases
{
    public class PurchasedProduct
    {
        public Purchase Purchase { get; protected set; }
        public Product Product { get; protected set; }
        public int Quantity { get; protected set; }

        public static PurchasedProduct Create(Purchase purchase, CartProduct cartProduct)
        {
            return new PurchasedProduct()
            {
                Product = cartProduct.Product,
                Purchase = purchase,
                Quantity = cartProduct.Quantity
            };
        }
    }
}
