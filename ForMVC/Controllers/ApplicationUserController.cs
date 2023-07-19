using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskProject.Core.Features.Products.Commands.Models;
using TaskProject.Core.Features.Products.Queries.Models;
using TaskProject.Core.Features.UserCQRS.Command.Models;
using TaskProject.Core.Features.UserCQRS.Query.Models;

namespace TaskProject.Api.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ApplicationUserController : Controller
    {
        private readonly IMediator _mediator;

        public ApplicationUserController(IMediator mediator)
        {
            _mediator=mediator;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
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
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Redirect to a success page or perform any other necessary action
                return RedirectToAction("Index", "Authentication");
            }

            // Failed login, redirect to login page with error query parameter
            return RedirectToAction("Index", "ApplicationUser");
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
