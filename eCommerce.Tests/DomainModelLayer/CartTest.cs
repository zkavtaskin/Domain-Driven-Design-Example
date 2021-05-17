using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using eCommerce.DomainModelLayer.Carts;
using eCommerce.DomainModelLayer.Countries;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Products;
using eCommerce.DomainModelLayer.Services;
using eCommerce.Helpers.Domain;

namespace eCommerce.Tests.DomainModelLayer
{
    [TestClass]
    public class CartTest
    {
        [TestMethod, TestCategory("Unit")]
        public void SharedCart_SharingACart_ReturnsCorrectCustomerId()
        {
            var cart = CreateCart();
            var recipient = CreateCustomer("receiver");

            var sharedCart = cart.Share(recipient);

            recipient.Id.Should().Equals(sharedCart.CustomerId);
        }

        [TestMethod, TestCategory("Unit")]
        public void TotalCost_TwoProductsOneQuantitySummedUp_ReturnsCorrectSum()
        {
            var cart = CreateCart();

            var expected = 200;
            var actual = cart.TotalCost;
            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void TotalCost_TwoProductsTwoQuantitiesSummedUp_ReturnsCorrectTotalCostSum()
        {
            var cart = CreateCart();

            var expected = 200;
            var actual = cart.TotalCost;
            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void TotalCost_TwoSingleProductsTaxSummedUp_ReturnsCorrectTotalTaxSum()
        {
            var cart = CreateCart();

            var expected = 20;
            var actual = cart.TotalTax;
            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void SharedCart_SharingACart_ReturnsCorrectProducts()
        {
            var cart = CreateCart();
            var recipient = CreateCustomer("receiver");

            var sharedCart = cart.Share(recipient);

            var subjectProductIds = cart.Products.Select(x => x.ProductId).ToList();
            var sharedCartIds = sharedCart.Products.Select(x => x.ProductId).ToList();
            subjectProductIds.ForEach(id => Assert.IsTrue(sharedCartIds.Contains(id)));
        }
        

        [TestMethod, TestCategory("Unit")]
        public void SharedCart_SharingACart_ReturnsCorrectTotalTax()
        {
            var cart = CreateCart();
            var recipient = Customer.Create("receiver", "v", "c@c.com", new Mock<Country>().Object);

            var sharedCart = cart.Share(recipient);

            var expected = 20;
            var actual = sharedCart.TotalTax;
            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void SharedCart_SharingACart_ReturnsCorrectTotalCost()
        {
            var cart = CreateCart();
            var recipient = CreateCustomer("recipient");

            var sharedCart = cart.Share(recipient);

            var expected = 200;
            var actual = sharedCart.TotalCost;
            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void Add_AddingACartProduct_UpdatesCartProductsCorrectly()
        {
            var customer = CreateCustomer("customer");
            var cart = Cart.Create(customer);
            var product = Product.Create("Cheese Slices", 1, 99, ProductCode.Create("CheeseAbc"));
            var expectedCartProduct = CartProduct.Create(customer, cart, product, 1, new Mock<ITaxService>().Object);

            cart.Add(expectedCartProduct);

            var contains = cart.Products.Any(p => p.ProductId == product.Id);
            contains.Should().BeTrue();
        }


        [TestMethod, TestCategory("Unit")]
        public void Add_AddingACartProduct_RaisesProductAddedEvent()
        {

            var cart = CreateCart();
            var cartProduct = CreateCartProduct();
            var expectedEvent = new ProductAddedCart {CartProduct = cartProduct};
            ProductAddedCart actualEvent = null;
            DomainEvents.Register<ProductAddedCart>(addedCart => actualEvent = addedCart);

            cart.Add(cartProduct);

            actualEvent.Should().NotBeNull().And.Equals(expectedEvent);
        }


        [TestMethod, TestCategory("Unit")]
        public void Remove_RemovingACartProduct_RemovesCartProductsCorrectly()
        {
            var customer = CreateCustomer("receiver");
            var cart = Cart.Create(customer);
            var product = Product.Create("Cheese Slices", 1, 99, ProductCode.Create("CheeseAbc"));
            var cartProduct = CartProduct.Create(customer, cart, product, 1, new Mock<ITaxService>().Object);
            cart.Add(cartProduct);
            cart.Remove(product);

            var contains = cart.Products.Contains(cartProduct);

            contains.Should().BeFalse();
        }

        [TestMethod, TestCategory("Unit")]
        public void Clear_ClearingACart_ClearsAllProducts()
        {
            var customer = Customer.Create("receiver", "v", "c@c.com", new Mock<Country>().Object);
            var cart = Cart.Create(customer);
            var product = CreateCartProduct();
            cart.Add(product);

            cart.Clear();

            cart.Products.Count.Should().Be(0);
        }

        [TestMethod, TestCategory("Unit")]
        public void Create_CreatingANewCart_ReturnsACorrectCart()
        {
            var customer = Customer.Create("receiver", "v", "c@c.com", new Mock<Country>().Object);

            var cart = Cart.Create(customer);

            cart.Should().NotBeNull();
        }

        [TestMethod, TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_CreatingANewCart_ThrowsExceptionOnNullCustomer()
        {
            Customer customer = null;
            var cart = Cart.Create(customer);
        }

        [TestMethod, TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_RemovingACartProduct_ThrowsExceptionOnNullProduct()
        {
            var cart = CreateCart();
            Product nullProduct = null;
            cart.Remove(nullProduct);
        }

        [TestMethod, TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_AddingACartProduct_ThrowsExceptionOnNullProduct()
        {
            var customer = Customer.Create("receiver", "v", "c@c.com", new Mock<Country>().Object);
            var cart = Cart.Create(customer);
            CartProduct cartProduct = null;
            cart.Add(cartProduct);
        }

        private static Cart CreateCart()
        {
            var product1 = CreateCartProduct();
            var product2 = CreateCartProduct();

            Mock<Cart> cart = new Mock<Cart>();
            cart.CallBase = true;
            cart.SetupGet(x => x.Products)
                .Returns(new ReadOnlyCollection<CartProduct>(new List<CartProduct>()
                {
                    product1,
                    product2
                }));

            return cart.Object;
        }

        private static CartProduct CreateCartProduct()
        {
            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            var productId = Guid.NewGuid();
            cartProduct.CallBase = true;
            cartProduct.SetupGet(x => x.Quantity).Returns(1);
            cartProduct.SetupGet(x => x.Tax).Returns(10);
            cartProduct.SetupGet(x => x.Cost).Returns(100);
            cartProduct.SetupGet(x => x.ProductId).Returns(productId);
            return cartProduct.Object;
        }

        private static Customer CreateCustomer(string name)
            => Customer.Create(name, name.Reverse().ToString(), $"{name}@email.com", new Mock<Country>().Object);
    }
}