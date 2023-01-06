using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Services.CartApi.DbContexts;
using Shop.Services.CartApi.Models;

namespace Shop.Services.CartApi.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public CartRepository(IMapper mapper, ApplicationDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async  Task<bool> ClearCart(string userId)
        {
            var cartHeaderFromDb = await _db.CartHeaders.FirstOrDefaultAsync(u=>u.UserId== userId);
            if (cartHeaderFromDb != null)
            {
                _db.CartDetails
                    .RemoveRange(_db.CartDetails.Where(u => u.CartHeaderId == cartHeaderFromDb.CartHeaderId));
                _db.CartHeaders.Remove(cartHeaderFromDb);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            Cart cart = _mapper.Map<CartDto, Cart>(cartDto);

            var prodInDb = _db.Products
                .FirstOrDefault(u => u.ProductId == cartDto.CartDetails.FirstOrDefault()
                .ProductId);
            if (prodInDb == null)
            {
                _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _db.SaveChangesAsync();   
            }
            var cartHEaderFromDb = await _db.CartHeaders.AsNoTracking()
                .FirstOrDefaultAsync(u=>u.UserId == cart.CartHeader.UserId);

            if (cartHEaderFromDb == null)
            {
                _db.CartHeaders.Add(cart.CartHeader);
                await _db.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                var  cartDatailFromDb = _db.CartDetails.AsNoTracking()
                    .FirstOrDefault(u=>u.ProductId == cart.CartDetails.FirstOrDefault().ProductId&&
                    u.CartHeaderId==cartHEaderFromDb.CartHeaderId);

                if (cartDatailFromDb == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();

                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDatailFromDb.Count;
                    _db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCartByUserID(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId)
            };
            cart.CartDetails = _db.CartDetails
                .Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId).Include(x=>x.Product);
            return _mapper.Map<CartDto>(cart);

        }

        public async Task<bool> RemoveFromCart(int cartDetailId)
        {
            try
            {
                CartDetails cartDetails = await _db.CartDetails.FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailId);
                int totalCountItems = _db.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();
                _db.CartDetails.Remove(cartDetails);
                if (totalCountItems == 1)
                {
                    var carttheaderToRemove = await _db.CartHeaders.FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    _db.CartHeaders.Remove(carttheaderToRemove);
                }
                await _db.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }
    }
}
