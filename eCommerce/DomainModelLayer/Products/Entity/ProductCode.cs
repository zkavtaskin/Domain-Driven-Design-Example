using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Domain;

namespace eCommerce.DomainModelLayer.Products
{
    public class ProductCode : IDomainEntity
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        public static ProductCode Create(string name)
        {
            return new ProductCode() 
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        public static ProductCode Create(Guid id, string name)
        {
            ProductCode productCode = Create(name);
            productCode.Id = id;
            return productCode;
        }
    }
}
