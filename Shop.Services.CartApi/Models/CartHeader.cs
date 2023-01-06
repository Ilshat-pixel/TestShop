namespace Shop.Services.CartApi.Models
{
    public class CartHeader
    {
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string? CouponCode { get; set; }
    }
}
