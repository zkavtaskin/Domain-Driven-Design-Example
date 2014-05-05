using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Repository;
using System.Linq.Expressions;
using eCommerce.Helpers.Specification;

namespace eCommerce.DomainModelLayer.Customers
{
    public class CustomerPurchasedNProductsSpec : SpecificationBase<Customer>
    {
        readonly int nPurchases;

        public CustomerPurchasedNProductsSpec(int nPurchases)
        {
            this.nPurchases = nPurchases;
        }

        public override Expression<Func<Customer, bool>> SpecExpression
        {
            get
            {
                return customer => customer.Purchases.Count == this.nPurchases
                    && customer.Active;
            }
        }
    }
}
