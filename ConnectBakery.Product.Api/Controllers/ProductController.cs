using ConnectBakery.Application.Services;
using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Application.Shared.Interfaces;
using ConnectBakery.Common.Enum;
using ConnectBakery.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ConnectBakery.Stock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
       

        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<ProductController>>();
            _productService = serviceProvider.GetRequiredService<IProductService>();
        }

        [HttpGet]
        [Route("get-products")]
        public async Task<IActionResult> GetProduts()
        {
            try
            {
                var products = await _productService.GetAll();

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType() , message = ex.Message});
            }
           
        }
        [HttpGet]
        [Route("{type}get-products-type")]
        public async Task<IActionResult> GetProduts(ProductType type)
        {
            try
            {
                var products = await _productService.GetByProductType(type);

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }

        }

        [HttpPost]
        [Route("create-product")]
        public async Task<IActionResult> CreateProduct(ProductDto product)
        {
            if(product is null)
                return BadRequest(new {message = "product est null"});

            try
            {
                await _productService.Create(product);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }
            
        }

        [HttpPut]
        [Route("update-product")]
        public async Task<IActionResult> UpdateProduct(ProductDto product)
        {
            if (product is null)
                return BadRequest(new { message = "product is null" });

            if(product.Id == Guid.Empty)
                return BadRequest($"Product {product.Id} not found");

            try
            {
                await _productService.Update(product);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest($"Product {id} not found");

            try
            {
                await _productService.Delete(id);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }
        }
    }
}
