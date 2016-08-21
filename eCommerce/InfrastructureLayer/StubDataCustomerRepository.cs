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
    public sealed class StubDataCustomerRepository : ICustomerRepository
    {
        readonly MemoryRepository<Customer> memRepository;

        public StubDataCustomerRepository(MemoryRepository<Customer> memRepository)
        {
            this.memRepository = memRepository;

            Customer customer = Customer.Create(new Guid("5D5020DA-47DF-4C82-A722-C8DEAF06AE23"), "john", "smith", "john.smith@microsoft.com",
                Country.Create(new Guid("229074BD-2356-4B5A-8619-CDEBBA71CC21"), "United Kingdom"));

            customer.Add(CreditCard.Create(customer, "MR J SMITH", "123122131", DateTime.Today.AddDays(1)));

            this.memRepository.Add(customer);
        }

        public Customer FindById(Guid id)
        {
            return this.memRepository.FindById(id);
        }

        public Customer FindOne(ISpecification<Customer> spec)
        {
            return this.memRepository.FindOne(spec);
        }

        public IEnumerable<Customer> Find(ISpecification<Customer> spec)
        {
            return this.memRepository.Find(spec);
        }

        public void Add(Customer entity)
        {
            this.memRepository.Add(entity);
        }

        public void Remove(Customer entity)
        {
            this.memRepository.Remove(entity);
        }

        public IEnumerable<CustomerPurchaseHistoryReadModel> GetCustomersPurchaseHistory()
        {
            //Here you either call a SQL view, do HQL joins, etc.
            //This returns your read model
            throw new NotImplementedException();
        }
    }
}
