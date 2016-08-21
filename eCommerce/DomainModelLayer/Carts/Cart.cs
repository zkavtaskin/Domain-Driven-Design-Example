using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Domain;
using System.Collections.ObjectModel;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Products;
using eCommerce.Helpers.Repository;
using eCommerce.DomainModelLayer.Purchases;
using eCommerce.Helpers.Specification;

namespace eCommerce.DomainModelLayer.Carts
{
    public class Cart : IDomainEntity
    {
        public virtual Guid Id { get; protected set; }

        private List<CartProduct> cartProducts = new List<CartProduct>();

        public virtual ReadOnlyCollection<CartProduct> Products
        {
            get { return cartProducts.AsReadOnly(); }
        }

        public virtual Customer Customer { get; protected set; }

        public virtual decimal TotalCost
        {
            get
            {
                return this.Products.Sum(cartProduct =>cartProduct.Quantity * cartProduct.Product.Cost);
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
            cart.Customer = customer;

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

        public virtual Nullable<ProductIssues> IsPurchaseReady()
        {
            ISpecification<Product> faultyProductSpec = new ProductReturnReasonSpec(ReturnReason.Faulty);

            foreach (CartProduct cartProduct in this.Products)
            {
                bool isInStock = new ProductIsInStockSpec(cartProduct)
                    .IsSatisfiedBy(cartProduct.Product);

                if (!isInStock)
                    return ProductIssues.NotInStock;

                bool isFaulty = faultyProductSpec.IsSatisfiedBy(cartProduct.Product);

                if (isFaulty)
                    return ProductIssues.IsFaulty;
            }
            return null;
        }

        public virtual Nullable<CheckOutIssue> IsCheckOutReady()
        {
            Nullable<PaymentIssues> paymentIssues = this.Customer.IsPayReady();

            if (paymentIssues.HasValue)
                return (CheckOutIssue)paymentIssues.Value;

            Nullable<ProductIssues> productIssue = this.IsPurchaseReady();

            if (productIssue.HasValue)
                return (CheckOutIssue)productIssue.Value;

            return null;
        }

        public virtual Purchase Checkout()
        {
            Nullable<CheckOutIssue> checkoutIssue = this.IsCheckOutReady();
            if (checkoutIssue.HasValue)
                throw new Exception(checkoutIssue.Value.ToString());

            Nullable<ProductIssues> productIssue = this.IsPurchaseReady();
            if (productIssue.HasValue)
                throw new Exception(productIssue.Value.ToString());

            Purchase purchase = Purchase.Create(this.Customer, this.Products);
            this.Customer.Add(purchase);
            DomainEvents.Raise<CustomerCheckedOut>(new CustomerCheckedOut() { Purchase = purchase });
            return purchase;
        }
    }
}
