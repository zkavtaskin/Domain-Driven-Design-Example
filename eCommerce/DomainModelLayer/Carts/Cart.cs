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

        private readonly List<CartProduct> _cartProducts = new List<CartProduct>();

        public virtual ReadOnlyCollection<CartProduct> Products => _cartProducts.AsReadOnly();

        public virtual Guid CustomerId { get; protected set; }

        public virtual decimal TotalCost => Products.Sum(cartProduct => cartProduct.Quantity * cartProduct.Cost);

        public virtual decimal TotalTax => Products.Sum(cartProducts => cartProducts.Tax);

        public static Cart Create(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            Cart cart = new Cart();
            cart.Id = Guid.NewGuid();
            cart.CustomerId = customer.Id;

            DomainEvents.Raise(new CartCreated { Cart = cart });

            return cart;
        }

        public virtual void Add(CartProduct cartProduct)
        {
            if (cartProduct == null)
                throw new ArgumentNullException();

            DomainEvents.Raise(new ProductAddedCart { CartProduct = cartProduct });

            _cartProducts.Add(cartProduct);
        }

        public virtual void Remove(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            CartProduct cartProduct =
                _cartProducts.Find(new ProductInCartSpec(product).IsSatisfiedBy);

            DomainEvents.Raise(new ProductRemovedCart() { CartProduct = cartProduct });

            _cartProducts.Remove(cartProduct);
        }

        public virtual void Clear() => _cartProducts.Clear();

        public Cart Share(Customer receiver)
        {
            var cart = Create(receiver);
            Products.ToList().ForEach(product => cart.Add(product));
            return cart;
        }
    }
}
