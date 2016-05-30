using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Repository;
using System.Linq.Expressions;
using eCommerce.Helpers.Specification;

namespace eCommerce.DomainModelLayer.Customers.Spec
{
    public class CreditCardAvailableSpec : SpecificationBase<CreditCard>
    {
        readonly DateTime dateTime;

        public CreditCardAvailableSpec(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        public override Expression<Func<CreditCard, bool>> SpecExpression
        {
            get
            {
                return creditCard => creditCard.Active && creditCard.Expiry >= this.dateTime;
            }
        }
    }
}
