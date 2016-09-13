using System;
using System.Linq.Expressions;
using eCommerce.Helpers.Specification;

namespace eCommerce.DomainModelLayer.Purchases
{
    public class CustomerPurchasesSpec : SpecificationBase<Purchase>
    {
        readonly Guid customerId;

        public CustomerPurchasesSpec(Guid customerId)
        {
            this.customerId = customerId;
        }

        public override Expression<Func<Purchase, bool>> SpecExpression
        {
            get
            {
                return purchase => purchase.CustomerId == this.customerId;
            }
        }
    }
}
