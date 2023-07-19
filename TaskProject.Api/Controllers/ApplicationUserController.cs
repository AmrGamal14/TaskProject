using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskProject.Core.Features.Products.Commands.Models;
using TaskProject.Core.Features.Products.Queries.Models;
using TaskProject.Core.Features.UserCQRS.Command.Models;
using TaskProject.Core.Features.UserCQRS.Query.Models;

namespace TaskProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationUserController(IMediator mediator)
        {
            _mediator=mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Paginated([FromQuery] GetUserListQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromForm] UpdateUserCommand command)
        {
            var response = await _mediator.Send(command);   
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var response = await _mediator.Send(new DeleteUserCommand(id));
            return Ok(response);
        }
        [HttpPut("/ChangeUserPassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangeUserPasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
