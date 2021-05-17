using System;
using System.Linq.Expressions;
using eCommerce.Helpers.Specification;

namespace eCommerce.DomainModelLayer.Carts
{
    public class CustomerCartSpec : SpecificationBase<Cart>
    {
        private readonly Guid _customerId;

        public CustomerCartSpec(Guid customerId)
        {
            _customerId = customerId;
        }

        public override Expression<Func<Cart, bool>> SpecExpression => 
            cart => cart.CustomerId == _customerId;
    }
}