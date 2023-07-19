using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskProject.Core.Features.Authorization.Commands.Models;

namespace TaskProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorizationController : ControllerBase

    {
        private readonly IMediator _mediator;

        public AuthorizationController(IMediator mediator)
        {
            _mediator=mediator;
        }
        [HttpPost]
        public async Task <IActionResult> Create([FromForm]AddRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
