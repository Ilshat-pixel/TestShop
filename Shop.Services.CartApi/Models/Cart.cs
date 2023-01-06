namespace Shop.Services.CartApi.Models
{
    public class Cart
    {
        public CartHeader CartHeader { get; set; }  

        public IEnumerable<CartDetails> CartDetails { get; set; }   
    }
}
