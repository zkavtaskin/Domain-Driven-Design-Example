using eCommerce.DomainModelLayer.Countries;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Products;
using eCommerce.Helpers.Repository;
using eCommerce.Helpers.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.InfrastructureLayer
{
    public sealed class StubDataProductRepository : IRepository<Product>
    {
        readonly MemoryRepository<Product> memRepository;

        public StubDataProductRepository(MemoryRepository<Product> memRepository)
        {
            this.memRepository = memRepository;

            this.memRepository.Add(Product.Create(new Guid("65D03D7E-E41A-49BC-8680-DC942BABD10A"), "iPhone", 2, 500.02m, 
                ProductCode.Create(new Guid("B2773EBF-CD0C-4F31-83E2-691973E32531"), "HD")));
        }

        public Product FindById(Guid id)
        {
            return this.memRepository.FindById(id);
        }

        public Product FindOne(ISpecification<Product> spec)
        {
            return this.memRepository.FindOne(spec);
        }

        public IEnumerable<Product> Find(ISpecification<Product> spec)
        {
            return this.memRepository.Find(spec);
        }

        public void Add(Product entity)
        {
            this.memRepository.Add(entity);
        }

        public void Remove(Product entity)
        {
            this.memRepository.Remove(entity);
        }
    }
}
