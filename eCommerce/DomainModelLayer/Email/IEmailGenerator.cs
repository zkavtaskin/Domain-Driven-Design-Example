using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace eCommerce.DomainModelLayer.Email
{
    public interface IEmailGenerator
    {
        MailMessage Generate(object objHolder, EmailTemplate emailTemplate);
    }
}
