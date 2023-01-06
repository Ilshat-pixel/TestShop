using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Web.Models;
using Shop.Web.Models.Dto;
using Shop.Web.Services.IServices;
using System.Diagnostics;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDto>("");
            if (response != null&&response.IsSucces)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));  
            }
            return View(list);
        }
        [Authorize]
        public async Task<IActionResult> Details(int productId)
        {
            ProductDto product = new();
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId,"");
            if (response != null && response.IsSucces)
            {
                product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            return View(product);
        }

        [HttpPost]
        [Authorize]
        [ActionName("Details")]

        public async Task<IActionResult> DetailsPost(ProductDto productDto)
        {
            CartDto cartDto = new CartDto()
            {
                CartHeader = new CartHeaderDto()
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
                }
            };
            CartDetailsDto cartDetail = new CartDetailsDto()
            {
                Count = productDto.Count,
                ProductId = productDto.ProductId,
            };
            var resp = await _productService.GetProductByIdAsync<ResponseDto>(productDto.ProductId,"");
            if (resp!=null && resp.IsSucces)
            {
                cartDetail.Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(resp.Result));
            }
            List<CartDetailsDto> cartDetailsDtos = new List<CartDetailsDto>
            {
                cartDetail
            };
            cartDto.CartDetails = cartDetailsDtos;

            var accesToken = await HttpContext.GetTokenAsync("access_token");
            var addTOCartResp = await _cartService.AddToCartAsync<ResponseDto>(cartDto, accesToken);
            if (addTOCartResp != null && addTOCartResp.IsSucces)
            {
                return RedirectToAction(nameof(Index));
            }
 
            return View(productDto);
        }
        [Authorize]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies","oidc");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}