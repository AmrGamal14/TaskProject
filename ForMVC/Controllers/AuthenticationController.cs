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
        private readonly ILocalStorageService _localStorageService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthenticationController(IMediator mediator, ILocalStorageService localStorageService, 
            IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _mediator=mediator;
            _localStorageService=localStorageService;
            this.httpContextAccessor=httpContextAccessor;
            _httpClientFactory = httpClientFactory;
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

                    _localStorageService.SetStorageValue("token", response.Data.AccessToken);
                    //HttpContext.Response.Headers.Add("Authorization", $"Bearer {response.Data.AccessToken}");
                    HttpContext.Session.SetString("Token",  response.Data.AccessToken);

                    //var httpClient = _httpClientFactory.CreateClient();
                    //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Data.AccessToken);

                    return RedirectToAction("Admin", "Product");
                }
                else if(response.Data.Roles.Contains("User") || response.Data.Roles.Contains("user"))
                {
                    //_localStorageService.SetStorageValue("token", response.Data.AccessToken);
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
