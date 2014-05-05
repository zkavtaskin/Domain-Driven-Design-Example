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


        public CustomerCheckedOutHandle(IEmailGenerator emailGenerator, IEmailDispatcher emailSender)
        {
            this.emailDispatcher = emailSender;
            this.emailGenerator = emailGenerator;
        }

        public void Handle(CustomerCheckedOut args)
        {
            this.emailDispatcher.Dispatch(
                this.emailGenerator.Generate(args.Purchase.Customer, EmailTemplate.PurchaseMade)
                );

            //send notifications, update third party systems, etc
        }
    }
}
