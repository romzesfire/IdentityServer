using System.Security.Claims;
using IdentityServer.DTO.Model;
using IdentityServer.Service.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IMediator _mediator;
        
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        [ProducesDefaultResponseType(typeof(AuthResponseDTO))]
        public async Task<IActionResult> Login([FromBody] AuthCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("CheckAuthorization")]
        [ProducesDefaultResponseType(typeof(ClaimsPrincipal))]
        public async Task<IActionResult> Login()
        {
            return Ok(HttpContext.User);
        }

        [HttpGet("RefreshTokens")]
        [ProducesDefaultResponseType(typeof(RefreshResponseDTO))]
        public async Task<IActionResult> RefreshTokens([FromQuery] RefreshTokenCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
