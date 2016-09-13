using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Domain;
using eCommerce.DomainModelLayer.Email;

namespace eCommerce.DomainModelLayer.Customers
{
    public class CustomerCheckedOutHandle : Handles<CustomerCheckedOut>
    {
        readonly IEmailDispatcher emailDispatcher;
        readonly IEmailGenerator emailGenerator;
        readonly ICustomerRepository customerRepository;

        public CustomerCheckedOutHandle(IEmailGenerator emailGenerator, 
            IEmailDispatcher emailSender, ICustomerRepository customerRepository)
        {
            this.emailDispatcher = emailSender;
            this.emailGenerator = emailGenerator;
            this.customerRepository = customerRepository;
        }

        public void Handle(CustomerCheckedOut args)
        {
            Customer customer = this.customerRepository.FindById(args.Purchase.CustomerId);

            this.emailDispatcher.Dispatch(
                this.emailGenerator.Generate(customer, EmailTemplate.PurchaseMade)
                );

            //send notifications, update third party systems, etc
        }
    }
}
