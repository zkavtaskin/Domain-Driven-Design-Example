using System;

namespace eCommerce.ApplicationLayer.Carts
{
    public interface ICartService
    {
        CartDto Add(Guid customerId, CartProductDto cartProductDto);
        CartDto Remove(Guid customerId, Guid productId);
        CartDto Get(Guid customerId);
        CheckOutResultDto CheckOut(Guid customerId);
        CartDto Share(Guid cartOwnerId, Guid cartRecipientId);
    }
}