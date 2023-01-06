using Shop.Web.Models.Dto;

namespace Shop.Web.Services.IServices
{
    public interface ICartService
    {
        Task<T> GetCartByUserIdAsync<T> (string userId,string token= null);
        Task<T> AddToCartAsync<T> (CartDto cartDto,string token= null);
        Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null);
        Task<T> RemoveFromCartASync<T>(int cartId, string token = null);



    }
}
