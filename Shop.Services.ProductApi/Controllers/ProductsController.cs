using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Services.ProductApi.Models.Dto;
using Shop.Services.ProductApi.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.Services.ProductApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        protected ResponseDto _response;
        private IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            this._response = new ResponseDto();
        }

        // GET: api/<ValuesController>
            [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                var product = await _productRepository.GetProducts();
                _response.Result= product;
            }
            catch (Exception ex)
            {

                _response.IsSucces = false;
                _response.ErrorMessages 
                    = new List<string>() { ex.ToString() }; 
            }
            return _response;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                var product = await _productRepository.GetProductById(id);
                _response.Result = product;
            }
            catch (Exception ex)
            {

                _response.IsSucces = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Authorize]
        public async Task<object> Post([FromBody] ProductDto productDto)
        {
            try
            {
                var product = await _productRepository.CreateUpdateProduct(productDto);
                _response.Result = product;
            }
            catch (Exception ex)
            {

                _response.IsSucces = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        [Authorize]
        public async Task<object>  Put( [FromBody] ProductDto productDto)
        {
            try
            {
                var product = await _productRepository.CreateUpdateProduct(productDto);
                _response.Result = product;
            }
            catch (Exception ex)
            {

                _response.IsSucces = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSucces = await _productRepository.DeleteProduct(id);
                _response.Result = isSucces;
            }
            catch (Exception ex)
            {

                _response.IsSucces = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
