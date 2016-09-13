using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using eCommerce.DomainModelLayer.Carts;
using eCommerce.DomainModelLayer.Products;

namespace eCommerce.Tests.DomainModelLayer
{
    [TestClass]
    public class CartTest
    {
        [TestMethod, TestCategory("Unit")]
        public void TotalCost_TwoProductsOneQuantitySummedUp_ReturnsCorrectSum()
        {
            decimal expected = 100;

            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            cartProduct.CallBase = true;
            cartProduct.SetupGet(x => x.Quantity).Returns(1);
            cartProduct.SetupGet(x => x.Cost).Returns(50);

            Mock<Cart> cart = new Mock<Cart>();
            cart.CallBase = true;
            cart.SetupGet(x => x.Products)
                .Returns(new ReadOnlyCollection<CartProduct>(new List<CartProduct>()
                    {
                        cartProduct.Object,
                        cartProduct.Object
                    }));

            decimal actual = cart.Object.TotalCost;

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void TotalCost_TwoProductsTwoQuantitiesSummedUp_ReturnsCorrectTotalCostSum()
        {
            decimal expected = 200;

            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            cartProduct.CallBase = true;
            cartProduct.SetupGet(x => x.Quantity).Returns(2);
            cartProduct.SetupGet(x => x.Cost).Returns(50);

            Mock<Cart> cart = new Mock<Cart>();
            cart.CallBase = true;
            cart.SetupGet(x => x.Products)
                .Returns(new ReadOnlyCollection<CartProduct>(new List<CartProduct>()
                    {
                        cartProduct.Object,
                        cartProduct.Object
                    }));

            decimal actual = cart.Object.TotalCost;

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void TotalCost_TwoSingleProductsTaxSummedUp_ReturnsCorrectTotalTaxSum()
        {
            decimal expected = 20;

            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            cartProduct.CallBase = true;
            cartProduct.SetupGet(x => x.Quantity).Returns(1);
            cartProduct.SetupGet(x => x.Tax).Returns(10);
            cartProduct.SetupGet(x => x.Cost).Returns(100);

            Mock<Cart> cart = new Mock<Cart>();
            cart.CallBase = true;
            cart.SetupGet(x => x.Products)
                .Returns(new ReadOnlyCollection<CartProduct>(new List<CartProduct>()
                    {
                        cartProduct.Object,
                        cartProduct.Object
                    }));

            decimal actual = cart.Object.TotalTax;

            actual.ShouldBeEquivalentTo(expected);
        }

    }
}
