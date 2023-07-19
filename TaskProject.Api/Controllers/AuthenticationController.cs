using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskProject.Core.Features.Authentication.Command.Models;
using TaskProject.Core.Features.Authentication.Queries.Models;

namespace TaskProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator=mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] SignInCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);

        }
        [HttpPost("/RefreshToken")]
        public async Task <IActionResult> RefreshToken([FromForm]RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok (response);

        }
        [HttpGet]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);

        }
    }
}
