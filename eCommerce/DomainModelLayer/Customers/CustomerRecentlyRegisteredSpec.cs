using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Repository;
using System.Linq.Expressions;
using eCommerce.Helpers.Specification;

namespace eCommerce.DomainModelLayer.Customers
{
    public class CustomerRecentlyRegisteredSpec : SpecificationBase<Customer>
    {
        readonly int nDays;

        public CustomerRecentlyRegisteredSpec(int nDays)
        {
            this.nDays = nDays;
        }

        public override Expression<Func<Customer, bool>> SpecExpression
        {
            get
            {
                return customer => customer.Created <= DateTime.Today.AddDays(nDays)
                    && customer.Active;
            }
        }
    }
}
