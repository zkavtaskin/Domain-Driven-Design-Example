using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Products;

namespace eCommerce.DomainModelLayer.Services
{
    public interface ITaxService
    {
        decimal Calculate(Customer customer, Product product);
    }
}
