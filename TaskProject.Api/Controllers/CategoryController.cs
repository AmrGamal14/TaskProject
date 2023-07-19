using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskProject.Core.Features.Categorys.Commands.Models;
using TaskProject.Core.Features.Categorys.Queries.Models;
using TaskProject.Core.Features.Products.Commands.Models;
using TaskProject.Core.Features.Products.Queries.Models;

namespace TaskProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator=mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoryList()
        {
            var response = await _mediator.Send(new GetCategoryListQuery());
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddCategoryCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var response = await _mediator.Send(new DeleteCategoryCommand(id));
            return Ok(response);
        }
    }
}
