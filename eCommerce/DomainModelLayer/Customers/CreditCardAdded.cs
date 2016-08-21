using eCommerce.Helpers.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DomainModelLayer.Customers
{
    public class CreditCardAdded : DomainEvent
    {
        public CreditCard CreditCard { get; set; }

        public override void Flatten()
        {
            this.Args.Add("CustomerId", this.CreditCard.Customer.Id);
            this.Args.Add("NameOnCard", this.CreditCard.NameOnCard);
            this.Args.Add("Last3Digits", this.CreditCard.CardNumber.Substring(this.CreditCard.CardNumber.Length - 3, 3));
        }
    }
}
