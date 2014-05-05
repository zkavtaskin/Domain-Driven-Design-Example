using eCommerce.DomainModelLayer.Countries;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Tax;
using eCommerce.Helpers.Repository;
using eCommerce.Helpers.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.InfrastructureLayer
{
    public sealed class StubDataCountryTaxRepository : IRepository<CountryTax>
    {
        readonly MemoryRepository<CountryTax> memRepository;

        public StubDataCountryTaxRepository(MemoryRepository<CountryTax> memRepository)
        {
            this.memRepository = memRepository;

            Country countryUK = Country.Create(new Guid("229074BD-2356-4B5A-8619-CDEBBA71CC21"), "United Kingdom");
            Country countryUS = Country.Create(new Guid("F3C78DD5-026F-4402-8A19-DAA956ACE593"), "United States");

            this.memRepository.Add(CountryTax.Create(new Guid("6A506865-AF49-496C-BFE1-747759B76F4A"), TaxType.Business, countryUK, 0.05m));
            this.memRepository.Add(CountryTax.Create(new Guid("D8B4C943-FCB7-4718-A56E-8B30D02992E7"), TaxType.Customer, countryUK, 0.10m));
            this.memRepository.Add(CountryTax.Create(new Guid("7F7D433B-3052-446F-99BF-A3514B7C50BA"), TaxType.Business, countryUS, 0.07m));
            this.memRepository.Add(CountryTax.Create(new Guid("1205C310-447A-48AA-A2F1-13B3E99C353E"), TaxType.Customer, countryUS, 0.15m));
        }

        public CountryTax FindById(Guid id)
        {
            return this.memRepository.FindById(id);
        }

        public CountryTax FindOne(ISpecification<CountryTax> spec)
        {
            return this.memRepository.FindOne(spec);
        }

        public IEnumerable<CountryTax> Find(ISpecification<CountryTax> spec)
        {
            return this.memRepository.Find(spec);
        }

        public void Add(CountryTax entity)
        {
            this.memRepository.Add(entity);
        }

        public void Remove(CountryTax entity)
        {
            this.memRepository.Remove(entity);
        }
    }
}
