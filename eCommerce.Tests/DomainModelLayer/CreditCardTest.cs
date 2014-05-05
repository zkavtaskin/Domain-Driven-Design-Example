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

namespace eCommerce.Tests.DomainModelLayer
{
    [TestClass]
    public class CreditCardTest
    {
        [TestMethod, TestCategory("Unit")]
        public void Create_WithAllInformation_ReturnsCorrectProject()
        {
            //Mock, don't open your "creditcard object" up just to set properties up.
            //Instead use Mock to setup returns. Mock creates a proxy object against which we can validate.
            Mock<CreditCard> expected = new Mock<CreditCard>();
            expected.SetupGet(x => x.NameOnCard).Returns("MR J SMITH");
            expected.SetupGet(x => x.CardNumber).Returns(293923910200292);
            expected.SetupGet(x => x.Active).Returns(true);
            expected.SetupGet(x => x.Created).Returns(DateTime.Today);
            expected.SetupGet(x => x.Expiry).Returns(DateTime.Today.AddDays(1));
            expected.SetupGet(x => x.Customer).Returns(new Customer());

            //call a method
            CreditCard actual = CreditCard.Create(new Customer(), "MR J SMITH",
                293923910200292, DateTime.Today.AddDays(1));

            //single assert thanks to fluent assertions framework
            actual.ShouldBeEquivalentTo(expected.Object);
        }

        [TestMethod, TestCategory("Unit")]
        [ExpectedException(typeof(Exception))]
        public void Create_ExpiredCreditCard_ThrowsException()
        {
            CreditCard actual = CreditCard.Create(new Customer(), "MR J SMITH",
                293923910200292, DateTime.Today.AddDays(-1));
        }

        [TestMethod, TestCategory("Unit")]
        [ExpectedException(typeof(Exception))]
        public void Create_DuplicateCreditCard_ThrowsException()
        {
            //setup
            Mock<CreditCard> creditCard = new Mock<CreditCard>() 
            {
                CallBase = true
            };

            creditCard.SetupGet(x => x.CardNumber).Returns(293923910200292);
            creditCard.SetupGet(x => x.Expiry).Returns(DateTime.Today.AddDays(1));

            Mock<Customer> customer = new Mock<Customer>();
            customer.SetupGet(x => x.CreditCards)
                .Returns(new ReadOnlyCollection<CreditCard>(new List<CreditCard>() { creditCard.Object }));

            //cal
            CreditCard actual = CreditCard.Create(customer.Object, "MR J SMITH",
                293923910200292, DateTime.Today.AddDays(1));
        }

        [TestMethod, TestCategory("Unit")]
        [ExpectedException(typeof(Exception))]
        public void Create_CustomerIsNull_ThrowsException()
        {
            CreditCard.Create(null, "MR J SMITH", 293923910200292, DateTime.Today.AddDays(1));
        }

        [TestMethod, TestCategory("Unit")]
        [ExpectedException(typeof(Exception))]
        public void Create_NameIsNull_ThrowsException()
        {
            CreditCard.Create(new Customer(), null, 293923910200292, DateTime.Today.AddDays(1));
        }

        [TestMethod, TestCategory("Unit")]
        [ExpectedException(typeof(Exception))]
        public void Create_CardNumberIsZero_ThrowsException()
        {
            CreditCard.Create(new Customer(), "MR J SMITH", 0, DateTime.Today.AddDays(1));
        }
    }
}
