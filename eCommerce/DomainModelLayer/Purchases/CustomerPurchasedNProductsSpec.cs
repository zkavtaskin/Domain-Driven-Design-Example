using System;
using System.Linq.Expressions;
using eCommerce.Helpers.Specification;

namespace eCommerce.DomainModelLayer.Purchases
{
    public class PurchasedNProductsSpec : SpecificationBase<Purchase>
    {
        readonly int nProducts;

        public PurchasedNProductsSpec(int nProducts)
        {
            this.nProducts = nProducts;
        }

        public override Expression<Func<Purchase, bool>> SpecExpression
        {
            get
            {
                return purchase => purchase.Products.Count >= this.nProducts;
            }
        }
    }
}
