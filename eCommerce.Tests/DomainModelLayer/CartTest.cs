using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eCommerce.DomainModelLayer.Customers;
using Moq;
using FluentAssertions;
using eCommerce.DomainModelLayer.Purchases;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentAssertions.Equivalency;
using eCommerce.Helpers.Domain;
using eCommerce.DomainModelLayer.Carts;
using eCommerce.DomainModelLayer.Products;

namespace eCommerce.Tests.DomainModelLayer
{
    [TestClass]
    public class CartTest
    {
        [TestMethod, TestCategory("Unit")]
        public void IsPurchaseReady_CartEmpty_ReturnsNull()
        {
            Nullable<ProductIssues> expected = null;

            Mock<Cart> cart = new Mock<Cart>();
            cart.CallBase = true;
            cart.SetupGet(x => x.Products).Returns(new ReadOnlyCollection<CartProduct>(new List<CartProduct>()));

            Nullable<ProductIssues> actual = cart.Object.IsPurchaseReady();

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void IsPurchaseReady_CartReady_ReturnsNull()
        {
            Nullable<ProductIssues> expected = null;

            Mock<Product> product = new Mock<Product>();
            product.SetupGet(x => x.Active).Returns(true);
            product.SetupGet(x => x.Id).Returns(Guid.Empty);
            product.SetupGet(x => x.Quantity).Returns(1);
            product.SetupGet(x => x.Returns).Returns(new ReadOnlyCollection<Return>(new List<Return>()));

            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            cartProduct.CallBase = true;
            cartProduct.SetupGet(x => x.Quantity).Returns(1);
            cartProduct.SetupGet(x => x.Product).Returns(product.Object);

            Mock<Cart> cart = new Mock<Cart>();
            cart.CallBase = true;
            cart.SetupGet(x => x.Products)
                .Returns(new ReadOnlyCollection<CartProduct>(new List<CartProduct>()
                    {
                        cartProduct.Object
                    }));

            Nullable<ProductIssues> actual = cart.Object.IsPurchaseReady();

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void IsPurchaseReady_ProductNotInStock_ReturnsNotInStock()
        {
            Nullable<ProductIssues> expected = ProductIssues.NotInStock;

            Mock<Product> product = new Mock<Product>();
            product.SetupGet(x => x.Active).Returns(true);
            product.SetupGet(x => x.Id).Returns(Guid.Empty);
            product.SetupGet(x => x.Quantity).Returns(0);
            product.SetupGet(x => x.Returns).Returns(new ReadOnlyCollection<Return>(new List<Return>()));

            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            cartProduct.CallBase = true;
            cartProduct.SetupGet(x => x.Quantity).Returns(1);
            cartProduct.SetupGet(x => x.Product).Returns(product.Object);

            Mock<Cart> cart = new Mock<Cart>();
            cart.CallBase = true;
            cart.SetupGet(x => x.Products)
                .Returns(new ReadOnlyCollection<CartProduct>(new List<CartProduct>()
                    {
                        cartProduct.Object
                    }));

            Nullable<ProductIssues> actual = cart.Object.IsPurchaseReady();

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void IsPurchaseReady_ProductIsFaulty_ReturnsIsFaulty()
        {
            Nullable<ProductIssues> expected = ProductIssues.IsFaulty;

            Mock<Return> productReturn = new Mock<Return>();
            productReturn.SetupGet(x => x.Reason).Returns(ReturnReason.Faulty);

            Mock<Product> product = new Mock<Product>();
            product.SetupGet(x => x.Active).Returns(true);
            product.SetupGet(x => x.Id).Returns(Guid.Empty);
            product.SetupGet(x => x.Quantity).Returns(1);
            product.SetupGet(x => x.Returns).Returns(new ReadOnlyCollection<Return>(new List<Return>()
            {
                productReturn.Object
            }));

            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            cartProduct.CallBase = true;
            cartProduct.SetupGet(x => x.Quantity).Returns(1);
            cartProduct.SetupGet(x => x.Product).Returns(product.Object);

            Mock<Cart> cart = new Mock<Cart>();
            cart.CallBase = true;
            cart.SetupGet(x => x.Products)
                .Returns(new ReadOnlyCollection<CartProduct>(new List<CartProduct>()
                    {
                        cartProduct.Object
                    }));

            Nullable<ProductIssues> actual = cart.Object.IsPurchaseReady();

            actual.ShouldBeEquivalentTo(expected);
        }

        [TestMethod, TestCategory("Unit")]
        public void TotalCost_TwoSingleProductsSummedUp_ReturnsCorrectSum()
        {
            decimal expected = 2;

            Mock<Product> product = new Mock<Product>();
            product.SetupGet(x => x.Active).Returns(true);
            product.SetupGet(x => x.Quantity).Returns(1);
            product.SetupGet(x => x.Cost).Returns(1);
            product.SetupGet(x => x.Returns)
                .Returns(new ReadOnlyCollection<Return>(new List<Return>()));

            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            cartProduct.CallBase = true;
            cartProduct.SetupGet(x => x.Quantity).Returns(1);
            cartProduct.SetupGet(x => x.Product).Returns(product.Object);

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
        public void TotalCost_TwoSingleProductsTotalCostSummedUp_ReturnsCorrectTotalCostSum()
        {
            decimal expected = 4;

            Mock<Product> product = new Mock<Product>();
            product.SetupGet(x => x.Active).Returns(true);
            product.SetupGet(x => x.Quantity).Returns(1);
            product.SetupGet(x => x.Cost).Returns(1);
            product.SetupGet(x => x.Returns)
                .Returns(new ReadOnlyCollection<Return>(new List<Return>()));

            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            cartProduct.CallBase = true;
            cartProduct.SetupGet(x => x.Quantity).Returns(2);
            cartProduct.SetupGet(x => x.Product).Returns(product.Object);

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
        public void TotalCost_TwoDoubleProductsTotalCostSummedUp_ReturnsCorrectTotalCostSum()
        {
            decimal expected = 4;

            Mock<Product> product = new Mock<Product>();
            product.SetupGet(x => x.Active).Returns(true);
            product.SetupGet(x => x.Quantity).Returns(1);
            product.SetupGet(x => x.Cost).Returns(1);
            product.SetupGet(x => x.Returns)
                .Returns(new ReadOnlyCollection<Return>(new List<Return>()));

            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            cartProduct.CallBase = true;
            cartProduct.SetupGet(x => x.Quantity).Returns(2);
            cartProduct.SetupGet(x => x.Product).Returns(product.Object);

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
            decimal expected = 2;

            Mock<CartProduct> cartProduct = new Mock<CartProduct>();
            cartProduct.CallBase = true;
            cartProduct.SetupGet(x => x.Quantity).Returns(1);
            cartProduct.SetupGet(x => x.Tax).Returns(1);
            cartProduct.SetupGet(x => x.Product).Returns(new Product());

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
