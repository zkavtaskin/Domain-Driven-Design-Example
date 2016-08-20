using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eCommerce.Helpers.Domain;
using eCommerce.DomainModelLayer.Purchases;
using System.Collections.ObjectModel;
using eCommerce.DomainModelLayer.Carts;
using eCommerce.DomainModelLayer.Customers.Spec;
using eCommerce.DomainModelLayer.Countries;

namespace eCommerce.DomainModelLayer.Customers
{
    public class Customer : IDomainEntity
    {
        private List<Purchase> purchases = new List<Purchase>();
        private List<CreditCard> creditCards = new List<CreditCard>();

        public virtual Guid Id { get; protected set; }
        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string Password { get; protected set; }
        public virtual DateTime Created { get; protected set; }
        public virtual bool Active { get; protected set; }
        public virtual decimal Balance { get; protected set; }
        public virtual Country Country { get; protected set; }

        public virtual ReadOnlyCollection<Purchase> Purchases { get { return this.purchases.AsReadOnly(); } }
        public virtual ReadOnlyCollection<CreditCard> CreditCards { get { return this.creditCards.AsReadOnly(); } }

        public virtual Cart Cart { get; protected set; }

        public virtual void ChangeEmail(string email)
        {
            if(this.Email != email)
            {
                this.Email = email;
                DomainEvents.Raise<CustomerChangedEmail>(new CustomerChangedEmail() { Customer = this });
            }
        }

        public static Customer Create(string firstname, string lastname, string email, Country country)
        {
            return Create(Guid.NewGuid(), firstname, lastname, email, country); ;
        }

        public static Customer Create(Guid id, string firstname, string lastname, string email, Country country)
        {
            if (string.IsNullOrEmpty(firstname))
                throw new ArgumentNullException("firstname");

            if (string.IsNullOrEmpty(lastname))
                throw new ArgumentNullException("lastname");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            if (country == null)
                throw new ArgumentNullException("country");

            Customer customer = new Customer()
            {
                Id = id,
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Active = true,
                Created = DateTime.Today,
                Country = country
            };

            DomainEvents.Raise<CustomerCreated>(new CustomerCreated() { Customer = customer });

            customer.Cart = Cart.Create(customer);

            return customer;
        }

        public virtual ReadOnlyCollection<CreditCard> GetCreditCardsAvailble()
        {
            return this.creditCards.FindAll(new CreditCardAvailableSpec(DateTime.Today).IsSatisfiedBy).AsReadOnly();
        }

        public Nullable<PaymentIssues> IsPayReady()
        {
            if (this.Balance < 0)
                return PaymentIssues.UnpaidBalance;

            if (this.GetCreditCardsAvailble().Count == 0)
                return PaymentIssues.NoActiveCreditCardAvailable;

            return null;
        }

        public virtual void Add(CreditCard creditCard)
        {
            this.creditCards.Add(creditCard);
        }

        internal virtual void Add(Purchase purchase)
        {
            this.purchases.Add(purchase);
        }
    }
}
