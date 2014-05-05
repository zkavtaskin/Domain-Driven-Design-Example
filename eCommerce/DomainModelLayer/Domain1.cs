using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using eCommerce_Sample.Util.Domain;

namespace eCommerce_Sample.Domain1
{

    public class Product
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public int Quantity { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime Modified { get; protected set; }
        public bool Active { get; protected set; }
    }

    public class Cart
    {
        private List<Product> products;

        public ReadOnlyCollection<Product> Products
        {
            get { return products.AsReadOnly(); }
        }

        public static Cart Create(List<Product> products)
        {
            Cart cart = new Cart();
            cart.products = products;
            return cart;
        }
    }

    public class Order
    {
        private List<Product> products = new List<Product>();

        public Guid Id { get; protected set; }
        public ReadOnlyCollection<Product> Products
        {
            get { return products.AsReadOnly(); }
        }
        public DateTime Created { get; protected set; }
        public Customer Customer { get; protected set; }

        public static Order Create(Customer customer, ReadOnlyCollection<Product> products)
        {
            Order order = new Order()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                products = products.ToList(),
                Customer = customer
            };
            return order;
        }
    }

    public class Customer
    {
        private List<Order> orders = new List<Order>();

        public Guid Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public ReadOnlyCollection<Order> Orders { get { return this.orders.AsReadOnly(); } }

        public Order Checkout(Cart cart)
        {
            Order order = Order.Create(this, cart.Products);
            this.orders.Add(order);
            return order;
        }

        public static Customer Create(string firstName, string lastName, string email)
        {
            Customer customer = new Customer()
            {
                 FirstName = firstName,
                 LastName = lastName,
                 Email = email
            };
            return customer;
        }
    }
   
    public class Main()
    {
        public void Init()
        {
            Cart cart = Cart.Create(new List<Product>() { new Product(), new Product() });
            Customer customer = Customer.Create("josh", "smith", "josh.smith@microsoft.com");
            Order order = customer.Checkout(cart);
        }
    }

    public class CustomerCheckedOut : IDomainEvent
    {
        public Order Order { get; set; }
    }

    public class CustomerCheckedOutHandle : Handles<CustomerCheckedOut>
    {
        public void Handle(CustomerCheckedOut args)
        {
            throw new NotImplementedException();
        }
    }


}
