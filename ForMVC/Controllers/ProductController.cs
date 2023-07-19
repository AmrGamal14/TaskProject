using ForMVC;
using ForMVC.Action_Filter;
using ForMVC.LocalStorageService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using TaskProject.Core.Features.Products.Commands.Models;
using TaskProject.Core.Features.Products.Queries.Models;
using TaskProject.Core.Features.Products.Queries.Results;
using TaskProject.Service.Abstracts;

namespace TaskProject.Api.Controllers
{
    [ServiceFilter(typeof(AuthenticationFilter))]
    //[BearerAttribute]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILocalStorageService _localStorageService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(IMediator mediator, ILocalStorageService localStorageService
            , IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _mediator=mediator;
            _localStorageService=localStorageService;
            _httpClientFactory=httpClientFactory;
            _httpContextAccessor=httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> User()
        {
            var response = await _mediator.Send(new GetProductListQuery());
            return View(response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Admin()
        {
            var token2 = Request.Cookies["token"];
            var token = _localStorageService.GetStorageValue<string>("token");
            _httpContextAccessor.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";
 
            var response = await _mediator.Send(new GetProductListQuery());
            return View(response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            return View();
        }        
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteProductCommand(id));
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteConfirmation()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var response = await _mediator.Send(new GetProductListQuery());
            return Ok(response);
        }
        [HttpGet("/SearchByCategoryId")]
        public async Task<IActionResult> GetProductListByCategoryID([FromQuery] GetProductListByCategoryId request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("/Paginating")]
        public async Task<IActionResult> Paginated([FromQuery] GetProductPaginatedListQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]AddProductCommand command)
        {
            var response = await _mediator.Send(command);
            return RedirectToAction("Product","Admin");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int productId)
        {
            var response = await _mediator.Send(new GetProductByIdQuery(productId));
            if (response != null)
            {
                return View(response.Data);
            }
            return RedirectToAction("Product", "Admin");

        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] EditProductCommand command)
        {
            var response = await _mediator.Send(command);
            return RedirectToAction("Product", "Admin");
        }
        
    }
}
