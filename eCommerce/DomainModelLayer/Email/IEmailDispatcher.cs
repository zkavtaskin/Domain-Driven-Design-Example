using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.DomainModelLayer.Customers;
using System.Net.Mail;

namespace eCommerce.DomainModelLayer.Email
{
    public interface IEmailDispatcher
    {
        void Dispatch(MailMessage mailMessage);
    }
}
