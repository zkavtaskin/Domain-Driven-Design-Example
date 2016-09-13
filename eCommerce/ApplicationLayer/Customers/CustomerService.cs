using eCommerce.Helpers.Repository;
using eCommerce.Helpers.Specification;
using eCommerce.DomainModelLayer.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.DomainModelLayer.Countries;
using eCommerce.DomainModelLayer.Purchases;

namespace eCommerce.ApplicationLayer.Customers
{
    public class CustomerService : ICustomerService
    {
        readonly ICustomerRepository customerRepository;
        readonly IRepository<Country> countryRepository;
        readonly IRepository<Purchase> purchaseRepository;
        readonly IUnitOfWork unitOfWork;

        public CustomerService(ICustomerRepository customerRepository,
            IRepository<Country> countryRepository, IRepository<Purchase> purchaseRepository, IUnitOfWork unitOfWork)
        {
            this.customerRepository = customerRepository;
            this.countryRepository = countryRepository;
            this.purchaseRepository = purchaseRepository;
            this.unitOfWork = unitOfWork;
        }

        public bool IsEmailAvailable(string email)
        {
            ISpecification<Customer> alreadyRegisteredSpec =
                new CustomerAlreadyRegisteredSpec(email);

            Customer existingCustomer = this.customerRepository.FindOne(alreadyRegisteredSpec);

            if (existingCustomer == null)
                return true;

            return false;
        }

        public CustomerDto Add(CustomerDto customerDto)
        {
            ISpecification<Customer> alreadyRegisteredSpec =
                new CustomerAlreadyRegisteredSpec(customerDto.Email);

            Customer existingCustomer = this.customerRepository.FindOne(alreadyRegisteredSpec);

            if (existingCustomer != null)
                throw new Exception("Customer with this email already exists");

            Country country = this.countryRepository.FindById(customerDto.CountryId);

            Customer customer =
                Customer.Create(customerDto.FirstName, customerDto.LastName, customerDto.Email, country);

            this.customerRepository.Add(customer);
            this.unitOfWork.Commit();

            return AutoMapper.Mapper.Map<Customer, CustomerDto>(customer);
        }

        public void Update(CustomerDto customerDto)
        {
            if (customerDto.Id == Guid.Empty)
                throw new Exception("Id can't be empty");

            ISpecification<Customer> registeredSpec =
                new CustomerRegisteredSpec(customerDto.Id);

            Customer customer = this.customerRepository.FindOne(registeredSpec);

            if (customer == null)
                throw new Exception("No such customer exists");

            customer.ChangeEmail(customerDto.Email);
            this.unitOfWork.Commit();
        }

        public void Remove(Guid customerId)
        {
            ISpecification<Customer> registeredSpec =
                new CustomerRegisteredSpec(customerId);

            Customer customer = this.customerRepository.FindOne(registeredSpec);

            if (customer == null)
                throw new Exception("No such customer exists");

            this.customerRepository.Remove(customer);
            this.unitOfWork.Commit();
        }

        public CustomerDto Get(Guid customerId)
        {
            ISpecification<Customer> registeredSpec =
                new CustomerRegisteredSpec(customerId);

            Customer customer = this.customerRepository.FindOne(registeredSpec);

            return AutoMapper.Mapper.Map<Customer, CustomerDto>(customer);
        }


        public CreditCardDto Add(Guid customerId, CreditCardDto creditCardDto)
        {
            ISpecification<Customer> registeredSpec =
                new CustomerRegisteredSpec(customerId);

            Customer customer = this.customerRepository.FindOne(registeredSpec);

            if (customer == null)
                throw new Exception("No such customer exists");

            CreditCard creditCard =
                CreditCard.Create(customer, creditCardDto.NameOnCard,
                creditCardDto.CardNumber, creditCardDto.Expiry);

            customer.Add(creditCard);

            this.unitOfWork.Commit();

            return AutoMapper.Mapper.Map<CreditCard, CreditCardDto>(creditCard);
        }

        //Approach 1 - Domain Model DTO Projection 
        public List<CustomerPurchaseHistoryDto> GetAllCustomerPurchaseHistoryV1()
        {
            IEnumerable<Guid> customersThatHavePurhcasedSomething =
                 this.purchaseRepository.Find(new PurchasedNProductsSpec(1))
                                        .Select(purchase => purchase.CustomerId)
                                        .Distinct();

            IEnumerable<Customer> customers =
                this.customerRepository.Find(new CustomerBulkIdFindSpec(customersThatHavePurhcasedSomething));

            List<CustomerPurchaseHistoryDto> customersPurchaseHistory =
                new List<CustomerPurchaseHistoryDto>();

            foreach (Customer customer in customers)
            {
                IEnumerable<Purchase> customerPurchases =
                    this.purchaseRepository.Find(new CustomerPurchasesSpec(customer.Id));

                CustomerPurchaseHistoryDto customerPurchaseHistory = new CustomerPurchaseHistoryDto();
                customerPurchaseHistory.CustomerId = customer.Id;
                customerPurchaseHistory.FirstName = customer.FirstName;
                customerPurchaseHistory.LastName = customer.LastName;
                customerPurchaseHistory.Email = customer.Email;
                customerPurchaseHistory.TotalPurchases = customerPurchases.Count();
                customerPurchaseHistory.TotalProductsPurchased =
                    customerPurchases.Sum(purchase => purchase.Products.Sum(product => product.Quantity));
                customerPurchaseHistory.TotalCost = customerPurchases.Sum(purchase => purchase.TotalCost);
                customersPurchaseHistory.Add(customerPurchaseHistory);

            }
            return customersPurchaseHistory;
        }

        //Approach 2 - Infrastructure Read Model Projection (Preferred)
        public List<CustomerPurchaseHistoryDto> GetAllCustomerPurchaseHistoryV2()
        {
            IEnumerable<CustomerPurchaseHistoryReadModel> customersPurchaseHistory =
                this.customerRepository.GetCustomersPurchaseHistory();

            return AutoMapper.Mapper.Map<IEnumerable<CustomerPurchaseHistoryReadModel>, List<CustomerPurchaseHistoryDto>>(customersPurchaseHistory);
        }
    }
}
