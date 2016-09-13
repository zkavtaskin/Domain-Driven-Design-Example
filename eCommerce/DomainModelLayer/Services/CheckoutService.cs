using eCommerce.DomainModelLayer.Carts;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Products;
using eCommerce.DomainModelLayer.Purchases;
using eCommerce.Helpers.Domain;
using eCommerce.Helpers.Repository;
using eCommerce.Helpers.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DomainModelLayer.Services
{
    public class CheckoutService : IDomainService
    {
        ICustomerRepository customerRepository;
        IRepository<Purchase> purchaseRepository;
        IRepository<Product> productRepository;

        public CheckoutService(ICustomerRepository customerRepository, 
            IRepository<Purchase> purchaseRepository, IRepository<Product> productRepository)
        {
            this.customerRepository = customerRepository;
            this.purchaseRepository = purchaseRepository;
            this.productRepository = productRepository;
        }

        public PaymentStatus CustomerCanPay(Customer customer)
        {
            if (customer.Balance < 0)
                return PaymentStatus.UnpaidBalance;

            if (customer.GetCreditCardsAvailble().Count == 0)
                return PaymentStatus.NoActiveCreditCardAvailable;

            return PaymentStatus.OK;
        }

        public ProductState ProductCanBePurchased(Cart cart)
        {
            ISpecification<Product> faultyProductSpec = new ProductReturnReasonSpec(ReturnReason.Faulty);

            foreach (CartProduct cartProduct in cart.Products)
            {
                Product product = this.productRepository.FindById(cartProduct.ProductId);
                if (product == null)
                    throw new Exception($"Product {cartProduct.ProductId} not found");

                bool isInStock = new ProductIsInStockSpec(cartProduct).IsSatisfiedBy(product);

                if (!isInStock)
                    return ProductState.NotInStock;

                bool isFaulty = faultyProductSpec.IsSatisfiedBy(product);

                if (isFaulty)
                    return ProductState.IsFaulty;
            }
            return ProductState.OK;
        }

        public Nullable<CheckOutIssue> CanCheckOut(Customer customer, Cart cart)
        {
            PaymentStatus paymentStatus = this.CustomerCanPay(customer);
            if (paymentStatus != PaymentStatus.OK)
                return (CheckOutIssue)paymentStatus;

            ProductState productState = this.ProductCanBePurchased(cart);
            if (productState != ProductState.OK)
                return (CheckOutIssue)productState;

            return null;
        }

        public Purchase Checkout(Customer customer, Cart cart)
        {
            Nullable<CheckOutIssue> checkoutIssue = this.CanCheckOut(customer, cart);
            if (checkoutIssue.HasValue)
                throw new Exception(checkoutIssue.Value.ToString());

            Purchase purchase = Purchase.Create(cart);

            this.purchaseRepository.Add(purchase);

            cart.Clear();

            DomainEvents.Raise<CustomerCheckedOut>(new CustomerCheckedOut() { Purchase = purchase });

            return purchase;
        }

    }
}
