using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.ApplicationLayer.Customers
{
    public interface ICustomerService
    {
        bool IsEmailAvailable(string email);
        CustomerDto Add(CustomerDto customerDto);
        void Update(CustomerDto customerDto);
        void Remove(Guid customerId);
        CustomerDto Get(Guid customerId);
        CreditCardDto Add(Guid customerId, CreditCardDto creditCard);
        List<CustomerPurchaseHistoryDto> GetAllCustomerPurchaseHistoryV1();

        List<CustomerPurchaseHistoryDto> GetAllCustomerPurchaseHistoryV2();
    }
}
