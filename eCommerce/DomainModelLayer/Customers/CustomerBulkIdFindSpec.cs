using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Repository;
using System.Linq.Expressions;
using eCommerce.Helpers.Specification;

namespace eCommerce.DomainModelLayer.Customers
{
    public class CustomerBulkIdFindSpec : SpecificationBase<Customer>
    {
        readonly IEnumerable<Guid> customerIds;

        public CustomerBulkIdFindSpec(IEnumerable<Guid> customerIds)
        {
            this.customerIds = customerIds;
        }

        public override Expression<Func<Customer, bool>> SpecExpression
        {
            get
            {
                return customer => this.customerIds.Contains(customer.Id);
            }
        }
    }
}
