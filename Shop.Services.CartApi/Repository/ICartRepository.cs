using Shop.Services.CartApi.Models;

namespace Shop.Services.CartApi.Repository
{
    public interface ICartRepository
    {
        Task<CartDto> GetCartByUserID(string userId);
        Task<CartDto> CreateUpdateCart(CartDto cartDto);
        Task<bool> RemoveFromCart(int cartDetailId);
        Task<bool> ClearCart(string userId);

    }
}
