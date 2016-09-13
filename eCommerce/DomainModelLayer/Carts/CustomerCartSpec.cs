using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Repository;
using eCommerce.DomainModelLayer.Products;
using System.Linq.Expressions;
using eCommerce.Helpers.Specification;

namespace eCommerce.DomainModelLayer.Carts
{
    public class CustomerCartSpec : SpecificationBase<Cart>
    {
        readonly Guid customerId;

        public CustomerCartSpec(Guid customerId)
        {
            this.customerId = customerId;
        }

        public override Expression<Func<Cart, bool>> SpecExpression
        {
            get
            {
                return cart => cart.CustomerId == this.customerId;
            }
        }
    }
}
