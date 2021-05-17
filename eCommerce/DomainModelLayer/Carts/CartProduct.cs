using System;
using eCommerce.DomainModelLayer.Products;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Services;

namespace eCommerce.DomainModelLayer.Carts
{
    public class CartProduct
    {
        public virtual Guid CartId { get; private set; }
        public virtual Guid CustomerId { get; private set; }
        public virtual int Quantity { get; private set; }
        public virtual Guid ProductId { get; private set; }
        public virtual DateTime Created { get; private set; }
        public virtual decimal Cost { get; private set; }
        public virtual decimal Tax { get; private set; }

        public static CartProduct Create(Customer customer, Cart cart, Product product, int quantity, ITaxService taxService)
        {
            if (cart == null)
                throw new ArgumentNullException(nameof(customer));

            if(cart == null)
                throw new ArgumentNullException(nameof(cart));

            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (taxService == null)
                throw new ArgumentNullException(nameof(taxService));

            CartProduct cartProduct = new CartProduct()
            {
                CustomerId = customer.Id,
                CartId = cart.Id,
                ProductId = product.Id,
                Quantity = quantity,
                Created = DateTime.Now,
                Cost = product.Cost,
                Tax = taxService.Calculate(customer, product)
            };

            return cartProduct;
        }
    }

}
