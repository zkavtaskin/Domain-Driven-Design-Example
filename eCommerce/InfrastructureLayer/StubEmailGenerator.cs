using eCommerce.DomainModelLayer.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace eCommerce.InfrastructureLayer
{
    public class StubEmailGenerator : IEmailGenerator
    {
        public MailMessage Generate(object objHolder, EmailTemplate emailTemplate)
        {
            //Generate your email here
            return new MailMessage();
        }
    }
}
