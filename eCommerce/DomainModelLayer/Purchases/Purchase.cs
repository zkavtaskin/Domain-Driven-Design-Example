using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Carts;

namespace eCommerce.DomainModelLayer.Purchases
{
    public class Purchase
    {
        private List<PurchasedProduct> purchasedProducts = new List<PurchasedProduct>();

        public Guid Id { get; protected set; }
        public ReadOnlyCollection<PurchasedProduct> Products
        {
            get { return purchasedProducts.AsReadOnly(); }
        }
        public DateTime Created { get; protected set; }
        public Customer Customer { get; protected set; }
        public decimal TotalTax { get; protected set; }
        public decimal TotalCost { get; protected set; }

        public static Purchase Create(Customer customer, ReadOnlyCollection<CartProduct> cartProducts)
        {
            Purchase purchase = new Purchase()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Today,
                Customer = customer,
                TotalCost = customer.Cart.TotalCost,
                TotalTax = customer.Cart.TotalTax
            };

            List<PurchasedProduct> purchasedProducts = new List<PurchasedProduct>();
            foreach (CartProduct cartProduct in cartProducts)
            {
                purchasedProducts.Add(PurchasedProduct.Create(purchase, cartProduct));
            }

            purchase.purchasedProducts = purchasedProducts;

            return purchase;
        }
    }
}
