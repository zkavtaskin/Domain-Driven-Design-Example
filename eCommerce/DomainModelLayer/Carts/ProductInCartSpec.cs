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
    public class ProductInCartSpec : SpecificationBase<CartProduct>
    {
        readonly Product product;

        public ProductInCartSpec(Product product)
        {
            this.product = product;
        }

        public override Expression<Func<CartProduct, bool>> SpecExpression
        {
            get
            {
                return cartProduct => cartProduct.ProductId == this.product.Id;
            }
        }
    }
}
