using ConnectBakery.Application.Services;
using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Application.Shared.Interfaces;
using ConnectBakery.Common.Constantes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConnectBakery.Stock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {


        private readonly ILogger<StockController> _logger;
        private readonly IStockService _stockService;
        private readonly IMediator _mediator;
        public StockController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<StockController>>();
            _stockService = serviceProvider.GetRequiredService<IStockService>();
            //_mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [HttpGet]
        [Route("get-stocks")]
        public async Task<IActionResult> GetStocks()
        {
            try
            {
                var stocks = await _stockService.GetAll();

                return Ok(stocks);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }

        }
        [HttpGet]
        [Route("{idProduct}/get-stock-product")]
        public async Task<IActionResult> GetOrdersClient(Guid idProduct)
        {
            try
            {
                var stock = await _stockService.GetByProduct(idProduct);

                return Ok(stock);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }

        }

        [HttpPost]
        [Route("create-stock")]
        public async Task<IActionResult> CreateStock(StockDto stock)
        {
            if (stock is null)
                return BadRequest(new { message = "stock est null" });
            try
            {
                return Ok(await _stockService.Create(stock));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }

        }

        [HttpPut]
        [Route("update-stock")]
        public async Task<IActionResult> UpdateStock(StockDto stock)
        {
            if (stock is null)
                return BadRequest(new { message = "stock is null" });

            if (stock.Id == Guid.Empty)
                return BadRequest($"Stock {stock.Id} {ResponseConstant.NotFound}");

            try
            {
                return Ok(await _stockService.Update(stock));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id}/delete-stock")]
        public async Task<IActionResult> DeleteStock(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest($"Stock {id} {ResponseConstant.NotFound}");

            try
            {
                return Ok(await _stockService.Delete(id));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }
        }
    }
}
