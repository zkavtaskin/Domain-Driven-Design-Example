using System;
using eCommerce.DomainModelLayer.Products;
using System.Linq.Expressions;
using eCommerce.Helpers.Specification;

namespace eCommerce.DomainModelLayer.Carts
{
    public class ProductInCartSpec : SpecificationBase<CartProduct>
    {
        private readonly Product _product;

        public ProductInCartSpec(Product product)
        {
            _product = product;
        }

        public override Expression<Func<CartProduct, bool>> SpecExpression =>
            cartProduct => cartProduct.ProductId == this._product.Id;
    }
}