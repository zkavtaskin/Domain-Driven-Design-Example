using eCommerce.DomainModelLayer.Carts;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Products;
using eCommerce.DomainModelLayer.Purchases;
using eCommerce.DomainModelLayer.Services;
using eCommerce.Helpers.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace eCommerce.Tests.DomainModelLayer
{
    [TestClass]
    public class CheckoutServiceTest
    {
        [TestMethod, TestCategory("Unit")]
        public void CustomerCanPay_BalanceIsNegative_ReturnsUnpaidBalance()
        {
            PaymentStatus expected = PaymentStatus.UnpaidBalance;

            Mock<Customer> customer = new Mock<Customer>();
            customer.SetupGet(x => x.Balance).Returns(-1);

            Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>();
            Mock<IRepository<Purchase>> purchaseRepository = new Mock<IRepository<Purchase>>();
            Mock<IRepository<Product>> productRepository = new Mock<IRepository<Product>>();
            CheckoutService checkoutService = new CheckoutService(customerRepository.Object, purchaseRepository.Object, 
                productRepository.Object);


            PaymentStatus actual = checkoutService.CustomerCanPay(customer.Object);

            Assert.AreEqual(expected, actual);    
        }

        [TestMethod, TestCategory("Unit")]
        public void CustomerCanPay_BalanceIsPositiveNoCreditCardsSetup_ReturnsNoActiveCards()
        {
            PaymentStatus expected = PaymentStatus.NoActiveCreditCardAvailable;

            Mock<Customer> customer = new Mock<Customer>();
            customer.SetupGet(x => x.Balance).Returns(0);
            customer.Setup(x => x.GetCreditCardsAvailble()).Returns(new List<CreditCard>() { }.AsReadOnly());

            Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>();
            Mock<IRepository<Purchase>> purchaseRepository = new Mock<IRepository<Purchase>>();
            Mock<IRepository<Product>> productRepository = new Mock<IRepository<Product>>();
            CheckoutService checkoutService = new CheckoutService(customerRepository.Object, purchaseRepository.Object,
                productRepository.Object);


            PaymentStatus actual = checkoutService.CustomerCanPay(customer.Object);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod, TestCategory("Unit")]
        public void CustomerCanPay_BalanceIsPositiveCreditCardsSetup_ReturnsOK()
        {
            PaymentStatus expected = PaymentStatus.OK;

            Mock<Customer> customer = new Mock<Customer>();
            customer.SetupGet(x => x.Balance).Returns(0);
            customer.Setup(x => x.GetCreditCardsAvailble())
                .Returns(
                    new List<CreditCard>()
                    {
                        new Mock<CreditCard>().Object
                    }.AsReadOnly()
                );

            Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>();
            Mock<IRepository<Purchase>> purchaseRepository = new Mock<IRepository<Purchase>>();
            Mock<IRepository<Product>> productRepository = new Mock<IRepository<Product>>();
            CheckoutService checkoutService = new CheckoutService(customerRepository.Object, purchaseRepository.Object, 
                productRepository.Object);

            PaymentStatus actual = checkoutService.CustomerCanPay(customer.Object);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod, TestCategory("Unit"), ExpectedException(typeof(Exception))]
        public void ProductCanBePurchased_ProductsNotFound_ThrowsException()
        {
            Mock<Cart> cart = new Mock<Cart>();
            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            cart.SetupGet(x => x.Products).Returns(new List<CartProduct>()
            {
                cartProduct.Object
            }.AsReadOnly());

            Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>();
            Mock<IRepository<Purchase>> purchaseRepository = new Mock<IRepository<Purchase>>();
            Mock<IRepository<Product>> productRepository = new Mock<IRepository<Product>>();
            CheckoutService checkoutService = new CheckoutService(customerRepository.Object, purchaseRepository.Object,
                productRepository.Object);

            checkoutService.ProductCanBePurchased(cart.Object);
        }

        [TestMethod, TestCategory("Unit")]
        public void ProductCanBePurchased_NotInStock_ReturnsNotInStock()
        {
            ProductState expected = ProductState.NotInStock;

            Mock<Cart> cart = new Mock<Cart>();
            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            cartProduct.SetupGet(x => x.Quantity).Returns(1);
            cart.SetupGet(x => x.Products).Returns(new List<CartProduct>()
            {
                cartProduct.Object
            }.AsReadOnly());

            Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>();
            Mock<IRepository<Purchase>> purchaseRepository = new Mock<IRepository<Purchase>>();
            Mock<IRepository<Product>> productRepository = new Mock<IRepository<Product>>();

            Mock<Product> product = new Mock<Product>();
            product.SetupGet(x => x.Quantity).Returns(0);
            product.SetupGet(x => x.Active).Returns(true);
            productRepository.Setup(x => x.FindById(It.IsAny<Guid>())).Returns(product.Object);

            CheckoutService checkoutService = new CheckoutService(customerRepository.Object, purchaseRepository.Object,
                productRepository.Object);

            ProductState actual = checkoutService.ProductCanBePurchased(cart.Object);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod, TestCategory("Unit")]
        public void ProductCanBePurchased_InStock_ReturnsOK()
        {
            ProductState expected = ProductState.OK;

            Mock<Cart> cart = new Mock<Cart>();
            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            cartProduct.SetupGet(x => x.Quantity).Returns(1);
            cart.SetupGet(x => x.Products).Returns(new List<CartProduct>()
            {
                cartProduct.Object
            }.AsReadOnly());

            Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>();
            Mock<IRepository<Purchase>> purchaseRepository = new Mock<IRepository<Purchase>>();
            Mock<IRepository<Product>> productRepository = new Mock<IRepository<Product>>();

            Mock<Product> product = new Mock<Product>();
            product.SetupGet(x => x.Quantity).Returns(1);
            product.SetupGet(x => x.Active).Returns(true);
            product.SetupGet(x => x.Returns).Returns(new List<Return>().AsReadOnly());

            productRepository.Setup(x => x.FindById(It.IsAny<Guid>())).Returns(product.Object);

            CheckoutService checkoutService = new CheckoutService(customerRepository.Object, purchaseRepository.Object,
                productRepository.Object);

            ProductState actual = checkoutService.ProductCanBePurchased(cart.Object);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod, TestCategory("Unit")]
        public void ProductCanBePurchased_NotEnoughStock_ReturnsNotInStock()
        {
            ProductState expected = ProductState.NotInStock;

            Mock<Cart> cart = new Mock<Cart>();
            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            cartProduct.SetupGet(x => x.Quantity).Returns(3);
            cart.SetupGet(x => x.Products).Returns(new List<CartProduct>()
            {
                cartProduct.Object
            }.AsReadOnly());

            Mock<ICustomerRepository> customerRepository = new Mock<ICustomerRepository>();
            Mock<IRepository<Purchase>> purchaseRepository = new Mock<IRepository<Purchase>>();
            Mock<IRepository<Product>> productRepository = new Mock<IRepository<Product>>();

            Mock<Product> product = new Mock<Product>();
            product.SetupGet(x => x.Quantity).Returns(1);
            product.SetupGet(x => x.Active).Returns(true);
            productRepository.Setup(x => x.FindById(It.IsAny<Guid>())).Returns(product.Object);

            CheckoutService checkoutService = new CheckoutService(customerRepository.Object, purchaseRepository.Object,
                productRepository.Object);

            ProductState actual = checkoutService.ProductCanBePurchased(cart.Object);

            Assert.AreEqual(expected, actual);
        }
    }
}
