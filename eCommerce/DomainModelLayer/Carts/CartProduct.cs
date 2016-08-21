using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.DomainModelLayer.Products;
using eCommerce.DomainModelLayer.Services;
using eCommerce.Helpers.Domain;

namespace eCommerce.DomainModelLayer.Carts
{
    public class CartProduct
    {
        public virtual Cart Cart { get; protected set; }
        public virtual int Quantity { get; protected set; }
        public virtual Product Product { get; protected set; }
        public virtual DateTime Created { get; protected set; }
        public virtual decimal Tax { get; set; }

        public static CartProduct Create(Cart cart, Product product, int quantity, ITaxService taxService)
        {
            if(cart == null)
                throw new ArgumentNullException("cart");

            if (product == null)
                throw new ArgumentNullException("product");

            CartProduct cartProduct = new CartProduct()
            {
                Cart = cart,
                Product = product,
                Quantity = quantity,
                Created = DateTime.Now,
                Tax = taxService.Calculate(cart.Customer, product)
            };

            return cartProduct;
        }
    }

}
