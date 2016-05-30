using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Domain;
using eCommerce.DomainModelLayer.Newsletter;
using eCommerce.DomainModelLayer.Email;
using System.Net.Mail;

namespace eCommerce.DomainModelLayer.Customers
{
    public class CustomerCreatedHandle : Handles<CustomerCreated>
    {
        readonly INewsletterSubscriber newsletterSubscriber;
        readonly IEmailDispatcher emailDispatcher;

        public CustomerCreatedHandle(INewsletterSubscriber newsletterSubscriber, IEmailDispatcher emailDispatcher)
        {
            this.newsletterSubscriber = newsletterSubscriber;
            this.emailDispatcher = emailDispatcher;
        }

        public void Handle(CustomerCreated args)
        {
            //example #1 calling an interface email dispatcher this can have differnet kind of implementation depending on context, e.g
            // smtp = SmtpEmailDispatcher, exchange = ExchangeEmailDispatcher, msmq = MsmqEmailDispatcher, etc...
            this.emailDispatcher.Dispatch(new MailMessage());

            //example #2 calling an interface newsletter subscriber  this can differnet kind of implementation e.g
            // web service = WSNewsletterSubscriber (current), msmq = MsmqNewsletterSubscriber, Sql = SqlNewsletterSubscriber, etc...
            this.newsletterSubscriber.Subscribe(args.Customer);
        }
    }
}
