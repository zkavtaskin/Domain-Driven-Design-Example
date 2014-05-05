using eCommerce.DomainModelLayer.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.ApplicationLayer.Products
{
    public interface IProductService
    {
        ProductDto Get(Guid productId);
        ProductDto Add(ProductDto product);
    }
}
