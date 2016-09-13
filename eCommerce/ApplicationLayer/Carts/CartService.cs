using System;
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
        IRepository<Customer> customerRepository;
        IRepository<Product> productRepository;
        IRepository<Cart> cartRepository;
        IUnitOfWork unitOfWork;
        TaxService taxDomainService;
        CheckoutService checkoutDomainService; 

        public CartService(IRepository<Customer> customerRepository, 
            IRepository<Product> productRepository, IRepository<Cart> cartRepository, 
            IUnitOfWork unitOfWork, TaxService taxDomainService, CheckoutService checkoutDomainService)
        {
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
            this.cartRepository = cartRepository;
            this.unitOfWork = unitOfWork;
            this.taxDomainService = taxDomainService;
            this.checkoutDomainService = checkoutDomainService;
        }

        public CartDto Add(Guid customerId, CartProductDto productDto)
        {
            CartDto cartDto = null;

            Customer customer = this.customerRepository.FindById(customerId);
            if (customer == null)
                throw new Exception(String.Format("Customer was not found with this Id: {0}", customerId));

            Cart cart = this.cartRepository.FindOne(new CustomerCartSpec(customerId));
            if(cart == null)
            {
                cart = Cart.Create(customer);
                this.cartRepository.Add(cart);
            }

            Product product = this.productRepository.FindById(productDto.ProductId);
            this.validateProduct(product.Id, product);

            //Double Dispatch Pattern
            cart.Add(CartProduct.Create(customer, cart, product, 
                productDto.Quantity, taxDomainService));

            cartDto = Mapper.Map<Cart, CartDto>(cart);
            this.unitOfWork.Commit();
            return cartDto;
        }

        public CartDto Remove(Guid customerId, Guid productId)
        {
            CartDto cartDto = null;

            Cart cart = this.cartRepository.FindOne(new CustomerCartSpec(customerId));
            this.validateCart(customerId, cart);

            Product product = this.productRepository.FindById(productId);
            this.validateProduct(productId, product);

            cart.Remove(product);
            cartDto = Mapper.Map<Cart, CartDto>(cart);
            this.unitOfWork.Commit();
            return cartDto;
        }

        public CartDto Get(Guid customerId)
        {
            Cart cart = this.cartRepository.FindOne(new CustomerCartSpec(customerId));
            this.validateCart(customerId, cart);

            return Mapper.Map<Cart, CartDto>(cart);
        }

        public CheckOutResultDto CheckOut(Guid customerId)
        {
            CheckOutResultDto checkOutResultDto = new CheckOutResultDto();

            Cart cart = this.cartRepository.FindOne(new CustomerCartSpec(customerId));
            this.validateCart(customerId, cart);

            Customer customer = this.customerRepository.FindById(cart.CustomerId);

            Nullable<CheckOutIssue> checkOutIssue = 
                this.checkoutDomainService.CanCheckOut(customer, cart);

            if (!checkOutIssue.HasValue)
            {
                Purchase purchase = this.checkoutDomainService.Checkout(customer, cart);
                checkOutResultDto = Mapper.Map<Purchase, CheckOutResultDto>(purchase);
                this.unitOfWork.Commit();
            }
            else
            {
                checkOutResultDto.CheckOutIssue = checkOutIssue;
            }

            return checkOutResultDto;
        }

        private void validateCart(Guid customerId, Cart cart)
        {
            if (cart == null)
                throw new Exception(String.Format("Customer was not found with this Id: {0}", customerId));
        }

        private void validateProduct(Guid productId, Product product)
        {
            if (product == null)
                throw new Exception(String.Format("Product was not found with this Id: {0}", productId));
        }
    }
}
