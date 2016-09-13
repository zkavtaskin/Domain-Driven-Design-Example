using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Repository;
using System.Linq.Expressions;
using eCommerce.Helpers.Specification;

namespace eCommerce.DomainModelLayer.Customers
{
    public class CustomerRegisteredSpec : SpecificationBase<Customer>
    {
        Guid id;

        public CustomerRegisteredSpec(Guid id)
        {
            this.id = id;
        }

        public override Expression<Func<Customer, bool>> SpecExpression
        {
            get
            {
                return customer => customer.Id == this.id;
            }
        }
    }
}
