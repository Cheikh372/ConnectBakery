using ConnectBakery.Application.Services;
using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Application.Shared.Interfaces;
using ConnectBakery.Common.Constantes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConnectBakery.Client.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientService _clientService;
        private readonly IMediator _mediator;
        public ClientController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<ClientController>>();
            _clientService = serviceProvider.GetRequiredService<IClientService>();
            //_mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [HttpGet]
        [Route("get-clients")]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var clients = await _clientService.GetAll();

                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }

        }


        [HttpPost]
        [Route("create-client")]
        public async Task<IActionResult> CreateClient(ClientDto client)
        {
            if (client is null)
                return BadRequest(new { message = "client est null" });

            try
            {
                return Ok(await _clientService.Create(client));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }

        }

        [HttpPut]
        [Route("update-client")]
        public async Task<IActionResult> UpdateClient(ClientDto client)
        {
            if (client is null)
                return BadRequest(new { message = "client is null" });

            if (client.Id == Guid.Empty)
                return BadRequest($"Client {client.Id} {ResponseConstant.NotFound}");

            try
            {
                return Ok(await _clientService.Update(client));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("delete-client/{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest($"Client {id} {ResponseConstant.NotFound}");

            try
            {
                return Ok(await _clientService.Delete(id));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }
        }

    }
}
