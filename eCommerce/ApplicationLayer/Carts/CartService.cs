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
        ITaxService taxDomainService;
        CheckoutService checkoutDomainService;

        public CartService(IRepository<Customer> customerRepository,
            IRepository<Product> productRepository, IRepository<Cart> cartRepository,
            IUnitOfWork unitOfWork, ITaxService taxDomainService, CheckoutService checkoutDomainService)
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
            ValidateCustomer(customerId, customer);

            Cart cart = this.cartRepository.FindOne(new CustomerCartSpec(customerId));
            if (cart == null)
            {
                cart = Cart.Create(customer);
                this.cartRepository.Add(cart);
            }

            Product product = this.productRepository.FindById(productDto.ProductId);
            this.ValidateProduct(product.Id, product);

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
            this.ValidateCart(customerId, cart);

            Product product = this.productRepository.FindById(productId);
            this.ValidateProduct(productId, product);

            cart.Remove(product);
            cartDto = Mapper.Map<Cart, CartDto>(cart);
            this.unitOfWork.Commit();
            return cartDto;
        }

        public CartDto Get(Guid customerId)
        {
            Cart cart = this.cartRepository.FindOne(new CustomerCartSpec(customerId));
            this.ValidateCart(customerId, cart);

            return Mapper.Map<Cart, CartDto>(cart);
        }

        public CheckOutResultDto CheckOut(Guid customerId)
        {
            CheckOutResultDto checkOutResultDto = new CheckOutResultDto();

            Cart cart = this.cartRepository.FindOne(new CustomerCartSpec(customerId));
            this.ValidateCart(customerId, cart);

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

        public CartDto Share(Guid cartOwnerId, Guid cartRecipientId)
        {
            var cart = this.cartRepository.FindOne(new CustomerCartSpec(cartOwnerId));
            this.ValidateCart(cartOwnerId, cart);

            var recipient = this.customerRepository.FindById(cartRecipientId);
            ValidateCustomer(cartRecipientId, recipient);

            var sharedCart = cart.Share(recipient);
            this.cartRepository.Add(sharedCart);

            return Mapper.Map<Cart, CartDto>(sharedCart);
        }

        private void ValidateCart(Guid customerId, Cart cart)
        {
            if (cart == null)
                throw new Exception(String.Format("Customer was not found with this Id: {0}", customerId));
        }

        private void ValidateProduct(Guid productId, Product product)
        {
            if (product == null)
                throw new Exception(String.Format("Product was not found with this Id: {0}", productId));
        }

        private void ValidateCustomer(Guid customerId, Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException($"Customer was not found with this Id: {customerId}");
        }
    }
}