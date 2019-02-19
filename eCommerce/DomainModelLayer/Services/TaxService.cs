using System;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Products;
using eCommerce.Helpers.Repository;
using eCommerce.DomainModelLayer.Tax;
using eCommerce.DomainModelLayer.Customers.Spec;
using eCommerce.Helpers.Domain;

namespace eCommerce.DomainModelLayer.Services
{
    public class TaxService : IDomainService, ITaxService
    {
        readonly IRepository<CountryTax> _countryTax;
        readonly Settings _settings;

        public TaxService(Settings settings, IRepository<CountryTax> countryTax)
        {
            _countryTax = countryTax;
            _settings = settings;
        }

        public decimal Calculate(Customer customer, Product product)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            if (product == null)
                throw new ArgumentNullException("product");

            CountryTax customerCountryTax = this._countryTax.FindOne(new CountryTypeOfTaxSpec(customer.CountryId, TaxType.Customer));
            CountryTax businessCountryTax = this._countryTax.FindOne(new CountryTypeOfTaxSpec(_settings.BusinessCountry.Id, TaxType.Business));

            return (product.Cost * customerCountryTax.Percentage)
                     + (product.Cost * businessCountryTax.Percentage);
        }
    }
}
