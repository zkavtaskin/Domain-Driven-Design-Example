using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Domain;
using System.Collections.ObjectModel;

namespace eCommerce.DomainModelLayer.Products
{
    public class Product : IDomainEntity
    {
        private List<Return> returns = new List<Return>();


        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual DateTime Created { get; protected set; }
        public virtual DateTime Modified { get; protected set; }
        public virtual bool Active { get; protected set; }
        public virtual int Quantity { get; protected set; }
        public virtual decimal Cost { get; protected set; }
        public virtual ProductCode Code { get; protected set; }
        public virtual ReadOnlyCollection<Return> Returns
        {
            get
            {
                return returns.AsReadOnly();
            }
        }

        public static Product Create(string name, int quantity, decimal cost, ProductCode productCode)
        {
            return new Product()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Quantity = quantity,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Active = true,
                Cost = cost,
                Code = productCode
            };
        }

        public static Product Create(Guid id, string name, int quantity, decimal cost, ProductCode productCode)
        {
            Product product = Create(name, quantity, cost, productCode);
            product.Id = id;
            return product;
        }
    }
}
