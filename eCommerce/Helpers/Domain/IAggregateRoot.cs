using System;

namespace eCommerce.Helpers.Domain
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}
