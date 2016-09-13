using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Repository;
using System.Linq.Expressions;
using eCommerce.DomainModelLayer.Carts;
using eCommerce.Helpers.Specification;

namespace eCommerce.DomainModelLayer.Products
{
    public class ProductIsInStockSpec : SpecificationBase<Product>
    {
        readonly CartProduct productCart;

        public ProductIsInStockSpec(CartProduct productCart)
        {
            this.productCart = productCart;
        }

        public override Expression<Func<Product, bool>> SpecExpression
        {

            get
            {
                return product => product.Id == this.productCart.ProductId && product.Active
                    && product.Quantity >= this.productCart.Quantity;
            }
        }
    }
}
