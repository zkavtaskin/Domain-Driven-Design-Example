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
    public sealed class StubDataProductCodeRepository : IRepository<ProductCode>
    {
        readonly MemoryRepository<ProductCode> memRepository;

        public StubDataProductCodeRepository(MemoryRepository<ProductCode> memRepository)
        {
            this.memRepository = memRepository;

            this.memRepository.Add(ProductCode.Create(new Guid("B2773EBF-CD0C-4F31-83E2-691973E32531"), "HD"));
            this.memRepository.Add(ProductCode.Create(new Guid("A4E934EF-C40D-41B3-87DF-C65F8DDD6C23"), "VK"));
        }

        public ProductCode FindById(Guid id)
        {
            return this.memRepository.FindById(id);
        }

        public ProductCode FindOne(ISpecification<ProductCode> spec)
        {
            return this.memRepository.FindOne(spec);
        }

        public IEnumerable<ProductCode> Find(ISpecification<ProductCode> spec)
        {
            return this.memRepository.Find(spec);
        }

        public void Add(ProductCode entity)
        {
            this.memRepository.Add(entity);
        }

        public void Remove(ProductCode entity)
        {
            this.memRepository.Remove(entity);
        }
    }
}
