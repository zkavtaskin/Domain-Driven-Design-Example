namespace eCommerce.WebService.Models
{
    public class AddProductToCartDto : CustomerProductDto
    {
        public int Quantity { get; set; }
    }
}