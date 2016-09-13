using System;
using System.Collections.Generic;
using System.Linq;
using eCommerce.Helpers.Domain;
using System.Collections.ObjectModel;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Products;

namespace eCommerce.DomainModelLayer.Carts
{
    public class Cart : IAggregateRoot
    {
        public virtual Guid Id { get; protected set; }

        private List<CartProduct> cartProducts = new List<CartProduct>();

        public virtual ReadOnlyCollection<CartProduct> Products
        {
            get { return cartProducts.AsReadOnly(); }
        }

        public virtual Guid CustomerId { get; protected set; }

        public virtual decimal TotalCost
        {
            get
            {
                return this.Products.Sum(cartProduct => cartProduct.Quantity * cartProduct.Cost);
            }
        }

        public virtual decimal TotalTax
        {
            get
            {
                return this.Products.Sum(cartProducts => cartProducts.Tax);
            }
        }

        public static Cart Create(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            Cart cart = new Cart();
            cart.Id = Guid.NewGuid();
            cart.CustomerId = customer.Id;

            DomainEvents.Raise<CartCreated>(new CartCreated() { Cart = cart });

            return cart;
        }

        public virtual void Add(CartProduct cartProduct)
        {
            if (cartProduct == null)
                throw new ArgumentNullException();

            DomainEvents.Raise<ProductAddedCart>(new ProductAddedCart() { CartProduct = cartProduct });

            this.cartProducts.Add(cartProduct);
        }

        public virtual void Remove(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            CartProduct cartProduct =
                this.cartProducts.Find(new ProductInCartSpec(product).IsSatisfiedBy);

            DomainEvents.Raise<ProductRemovedCart>(new ProductRemovedCart() { CartProduct = cartProduct });

            this.cartProducts.Remove(cartProduct);
        }

        public virtual void Clear()
        {
            this.cartProducts.Clear();
        }
    }
}
