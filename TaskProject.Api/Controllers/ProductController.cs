using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskProject.Core.Features.Products.Commands.Models;
using TaskProject.Core.Features.Products.Queries.Models;

namespace TaskProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator=mediator;
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
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromForm] EditProductCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        { 
            var response = await _mediator.Send(new DeleteProductCommand(id));
            return Ok(response);
        }
    }
}
