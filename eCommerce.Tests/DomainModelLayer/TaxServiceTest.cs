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
using eCommerce.DomainModelLayer.Services;
using eCommerce.DomainModelLayer;
using eCommerce.Helpers.Repository;
using eCommerce.DomainModelLayer.Tax;
using eCommerce.DomainModelLayer.Countries;
using eCommerce.DomainModelLayer.Products;
using eCommerce.DomainModelLayer.Customers.Spec;

namespace eCommerce.Tests.DomainModelLayer
{
    [TestClass]
    public class TaxServiceTest
    {
        [TestMethod, TestCategory("Unit")]
        public void Calculate_OverallProductTax_ReturnsCorrectTax()
        {
            //setup
            decimal expected = 7;

            Mock<Settings> settings = new Mock<Settings>();
            settings.SetupGet(x => x.BusinessCountry).Returns(new Country());
            
            Mock<Customer> customer = new Mock<Customer>();
            customer.SetupGet(x => x.CountryId).Returns(Guid.Empty);

            Mock<IRepository<CountryTax>> repositoryCountryTax = new Mock<IRepository<CountryTax>>();

            Mock<CountryTax> customerCountryTax = new Mock<CountryTax>();
            customerCountryTax.SetupGet(x => x.Percentage).Returns(0.05m);
            repositoryCountryTax.Setup(x => x.FindOne(new CountryTypeOfTaxSpec(Guid.Empty, TaxType.Customer))).Returns(customerCountryTax.Object);

            Mock<CountryTax> businessCountryTax = new Mock<CountryTax>();
            businessCountryTax.SetupGet(x => x.Percentage).Returns(0.02m);
            repositoryCountryTax.Setup(x => x.FindOne(new CountryTypeOfTaxSpec(Guid.Empty, TaxType.Business))).Returns(businessCountryTax.Object);

            Mock<Product> product = new Mock<Product>();
            product.SetupGet(x => x.Cost).Returns(100);
            product.SetupGet(x => x.Code).Returns(new ProductCode());

            //call method
            TaxService taxService = new TaxService(settings.Object, repositoryCountryTax.Object);

            decimal actual = taxService.Calculate(customer.Object, product.Object);

            //assert
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
