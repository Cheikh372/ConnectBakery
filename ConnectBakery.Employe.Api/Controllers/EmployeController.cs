using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Application.Shared.Interfaces;
using ConnectBakery.Common.Constantes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConnectBakery.Employe.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
   
    public class EmployeController : ControllerBase
    {
        private readonly ILogger<EmployeController> _logger;
        private readonly IEmployeService _employeService;
        private readonly IMediator _mediator;
        public EmployeController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<EmployeController>>();
            _employeService = serviceProvider.GetRequiredService<IEmployeService>();
            //_mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [HttpGet]
        [Route("get-employes")]
        public async Task<IActionResult> GetEmployes()
        {
            try
            {
                var employes = await _employeService.GetAll();

                return Ok(employes);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }

        }


        [HttpPost]
        [Route("create-employe")]
        public async Task<IActionResult> CreateEmploye(EmployeDto employe)
        {
            if (employe is null)
                return BadRequest(new { message = "employe est null" });
            try
            {
                return Ok(await _employeService.Create(employe));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }

        }

        [HttpPut]
        [Route("update-employe")]
        public async Task<IActionResult> UpdateEmploye(EmployeDto employe)
        {
            if (employe is null)
                return BadRequest(new { message = "employe is null" });

            if (employe.Id == Guid.Empty)
                return BadRequest($"Employe {employe.Id} {ResponseConstant.NotFound}");

            try
            {
                return Ok(await _employeService.Update(employe));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("delete-employe/{id}")]
        public async Task<IActionResult> DeleteEmploye(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest($"Employe {id} {ResponseConstant.NotFound}");

            try
            {
                return Ok(await _employeService.Delete(id));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }
        }

    }
    
}