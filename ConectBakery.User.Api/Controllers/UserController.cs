using ConnectBakery.Application.Shared.Dtos;
using ConnectBakery.Application.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConnectBakery.User.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {


        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        public UserController(IServiceProvider serviceProvider)
        {
            _logger = serviceProvider.GetRequiredService<ILogger<UserController>>();
            _userService = serviceProvider.GetRequiredService<IUserService>();
            //_mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        [HttpGet]
        [Route("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var clients = await _userService.GetAll();

                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }

        }


        [HttpPost]
        [Route("create-user")]
        public async Task<IActionResult> CreateUser(UserDto user)
        {
            if (user is null)
                return BadRequest(new { message = "user est null" });

            try
            {
                return Ok(await _userService.Create(user));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return BadRequest(new { type = ex.GetType(), message = ex.Message });
            }

        }

    }
}
