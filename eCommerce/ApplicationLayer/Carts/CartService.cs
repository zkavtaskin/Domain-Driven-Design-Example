using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Repository;
using eCommerce.DomainModelLayer.Customers;
using eCommerce.DomainModelLayer.Products;
using eCommerce.DomainModelLayer.Purchases;
using eCommerce.DomainModelLayer.Carts;
using eCommerce.DomainModelLayer.Services;
using AutoMapper;

namespace eCommerce.ApplicationLayer.Carts
{
    public class CartService : ICartService
    {
        readonly IRepository<Customer> repositoryCustomer;
        readonly IRepository<Product> repositoryProduct;
        readonly IUnitOfWork unitOfWork;
        readonly ITaxService taxDomainService;

        public CartService(IRepository<Customer> repositoryCustomer, 
            IRepository<Product> repositoryProduct, IUnitOfWork unitOfWork, ITaxService taxDomainService)
        {
            this.repositoryCustomer = repositoryCustomer;
            this.repositoryProduct = repositoryProduct;
            this.unitOfWork = unitOfWork;
            this.taxDomainService = taxDomainService;
        }

        public CartDto Add(Guid customerId, CartProductDto productDto)
        {
            CartDto cartDto = null;
            Customer customer = this.repositoryCustomer.FindById(customerId);
            this.validateCustomer(customerId, customer);

            Product product = this.repositoryProduct.FindById(productDto.ProductId);
            this.validateProduct(product.Id, product);

            //Double Dispatch Pattern
            customer.Cart.Add(CartProduct.Create(customer.Cart, product, 
                productDto.Quantity, taxDomainService));

            cartDto = Mapper.Map<Cart, CartDto>(customer.Cart);
            this.unitOfWork.Commit();
            return cartDto;
        }

        public CartDto Remove(Guid customerId, Guid productId)
        {
            CartDto cartDto = null;
            Customer customer = this.repositoryCustomer.FindById(customerId);
            this.validateCustomer(customerId, customer);

            Product product = this.repositoryProduct.FindById(productId);
            this.validateProduct(productId, product);

            customer.Cart.Remove(product);
            cartDto = Mapper.Map<Cart, CartDto>(customer.Cart);
            this.unitOfWork.Commit();
            return cartDto;
        }

        public CartDto Get(Guid customerId)
        {
            Customer customer = this.repositoryCustomer.FindById(customerId);
            this.validateCustomer(customerId, customer);
            return Mapper.Map<Cart, CartDto>(customer.Cart);
        }

        public CheckOutResultDto CheckOut(Guid customerId)
        {
            CheckOutResultDto checkOutResultDto = new CheckOutResultDto();
            Customer customer = this.repositoryCustomer.FindById(customerId);
            this.validateCustomer(customerId, customer);

            checkOutResultDto.CheckOutIssue = customer.Cart.IsCheckOutReady();

            if (!checkOutResultDto.CheckOutIssue.HasValue)
            {
                Purchase purchase = customer.Cart.Checkout();
                checkOutResultDto = Mapper.Map<Purchase, CheckOutResultDto>(purchase);
                this.unitOfWork.Commit();
            }

            return checkOutResultDto;
        }

        private void validateCustomer(Guid customerId, Customer customer)
        {
            if (customer == null)
                throw new Exception(String.Format("Customer was not found with this Id: {0}", customerId));
        }

        private void validateProduct(Guid productId, Product product)
        {
            if (product == null)
                throw new Exception(String.Format("Product was not found with this Id: {0}", productId));
        }
    }
}
