using ForMVC.LocalStorageService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using TaskProject.Core.Features.Authentication.Command.Models;
using TaskProject.Core.Features.Authentication.Queries.Models;

namespace ForMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator=mediator;
        
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(SignInCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //check role
                if(response.Data.Roles.Contains("Admin") || response.Data.Roles.Contains("admin"))
                {

                  
                    HttpContext.Session.SetString("Token",  response.Data.AccessToken);

                    return RedirectToAction("Admin", "Product");
                }
                else if(response.Data.Roles.Contains("User") || response.Data.Roles.Contains("user"))
                {
                  
                    return RedirectToAction("User", "Product");
                }
               
            }
            return RedirectToAction("Index", "Authentication", new { error = "incorrectPassword" });
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
