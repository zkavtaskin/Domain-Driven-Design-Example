using AutoMapper;
using eCommerce.ApplicationLayer.Carts;
using eCommerce.ApplicationLayer.Customers;
using eCommerce.ApplicationLayer.Products;
using eCommerce.DomainModelLayer.Carts;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Products;
using eCommerce.DomainModelLayer.Purchases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.ApplicationLayer
{
    public class Map : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Cart, CartDto>();
            Mapper.CreateMap<CartProduct, CartProductDto>()
                .ForMember(x => x.ProductId, options => options.MapFrom(x => x.Product.Id));

            Mapper.CreateMap<Purchase, CheckOutResultDto>()
                .ForMember(x => x.PurchaseId, options => options.MapFrom(x => x.Id));

            Mapper.CreateMap<CreditCard, CreditCardDto>();
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Product, ProductDto>();
            Mapper.CreateMap<CustomerPurchaseHistoryReadModel, CustomerPurchaseHistoryDto>();
        }
    }
}
