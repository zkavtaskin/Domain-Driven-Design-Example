using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Products;
using eCommerce.Helpers.Domain;

namespace eCommerce.DomainModelLayer.Services
{
    public interface ITaxService : IDomainService
    {
        decimal Calculate(Customer customer, Product product);
    }
}
