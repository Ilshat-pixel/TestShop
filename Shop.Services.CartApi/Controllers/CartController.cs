

using Microsoft.AspNetCore.Mvc;
using Shop.Services.CartApi.Models;
using Shop.Services.CartApi.Models.Dto;
using Shop.Services.CartApi.Repository;

namespace Shop.Services.CartApi.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController:Controller
    {
        private readonly ICartRepository _cartRepository;
        protected ResponseDto _responseDto;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            this._responseDto = new ResponseDto();
        }

        [HttpGet("GetCart/{userId}")]
        public async  Task<object> GetCart (string userId)
        {
            try
            {
                CartDto cartDto = await _cartRepository.GetCartByUserID(userId);
                _responseDto.Result = cartDto;
            }
            catch (Exception ex)
            {

                _responseDto.IsSucces = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _responseDto;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart([FromBody] CartDto cartDto)
        {
            try
            {
                CartDto cartDt = await _cartRepository.CreateUpdateCart(cartDto);
                _responseDto.Result = cartDt;
            }
            catch (Exception ex)
            {

                _responseDto.IsSucces = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _responseDto;
        }
        [HttpPut("UpdateCart")]
        public async Task<object> UpdateCart([FromBody] CartDto cartDto)
        {
            try
            {
                CartDto cartDt = await _cartRepository.CreateUpdateCart(cartDto);
                _responseDto.Result = cartDt;
            }
            catch (Exception ex)
            {

                _responseDto.IsSucces = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _responseDto;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCart([FromBody]int cartId)
        {
            try
            {
               bool isSuccces = await _cartRepository.RemoveFromCart(cartId);
                _responseDto.Result = isSuccces;
            }
            catch (Exception ex)
            {

                _responseDto.IsSucces = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _responseDto;
        }

        [HttpPost("ClearCart/{userId}")]
        public async Task<object> ClearCart(string userId)
        {
            try
            {
                bool isSuccces = await _cartRepository.ClearCart(userId);
                _responseDto.Result = isSuccces;
            }
            catch (Exception ex)
            {

                _responseDto.IsSucces = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _responseDto;
        }
    }
}
