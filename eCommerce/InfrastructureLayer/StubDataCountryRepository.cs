using eCommerce.DomainModelLayer.Countries;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.Helpers.Repository;
using eCommerce.Helpers.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.InfrastructureLayer
{
    public sealed class StubDataCountryRepository : IRepository<Country>
    {
        MemoryRepository<Country> memRepository;

        public StubDataCountryRepository(MemoryRepository<Country> memRepository)
        {
            this.memRepository = memRepository;

            memRepository.Add(Country.Create(new Guid("229074BD-2356-4B5A-8619-CDEBBA71CC21"), "United Kingdom"));
            memRepository.Add(Country.Create(new Guid("F3C78DD5-026F-4402-8A19-DAA956ACE593"), "United States"));
        }

        public Country FindById(Guid id)
        {
            return this.memRepository.FindById(id);
        }

        public Country FindOne(ISpecification<Country> spec)
        {
            return this.memRepository.FindOne(spec);
        }

        public IEnumerable<Country> Find(ISpecification<Country> spec)
        {
            return this.memRepository.Find(spec);
        }

        public void Add(Country entity)
        {
            this.memRepository.Add(entity);
        }

        public void Remove(Country entity)
        {
            this.memRepository.Remove(entity);
        }
    }
}
