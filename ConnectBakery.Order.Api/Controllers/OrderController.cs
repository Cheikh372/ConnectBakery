using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Application.Shared.Interfaces;
using ConnectBakery.Common.Constantes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConnectBakery.Stock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly IMediator _mediator;
        public OrderController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<OrderController>>();
            _orderService = serviceProvider.GetRequiredService<IOrderService>();
            //_mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [HttpGet]
        [Route("get-orders")]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var orders = await _orderService.GetAll();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }

        }

        [HttpGet]
        [Route("{idClient}/get-orders-client")]
        public async Task<IActionResult> GetOrdersClient(Guid idClient)
        {
            try
            {
                var orders = await _orderService.GetByClient(idClient);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }

        }

        [HttpPost]
        [Route("create-order")]
        public async Task<IActionResult> CreateOrder(OrderDto order)
        {
            if (order is null)
                return BadRequest(new { message = "order est null" });
            try
            {
                return Ok(await _orderService.Create(order));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }

        }

        [HttpPut]
        [Route("update-order")]
        public async Task<IActionResult> UpdateOrder(OrderDto order)
        {
            if (order is null)
                return BadRequest(new { message = "order is null" });

            if (order.Id == Guid.Empty)
                return BadRequest($"Order {order.Id} {ResponseConstant.NotFound}");

            try
            {
                return Ok(await _orderService.Update(order));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id}/delete-order")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest($"Order {id} {ResponseConstant.NotFound}");

            try
            {
                return Ok(await _orderService.Delete(id));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }
        }
    }
}
