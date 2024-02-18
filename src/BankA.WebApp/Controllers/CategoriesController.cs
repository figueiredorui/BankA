using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Application.UseCases.Categories.ListCategories.ListCategoriesResponse>> GetList([FromQuery] Application.UseCases.Categories.ListCategories.ListCategoriesRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application.UseCases.Categories.GetCategory.GetCategoryResponse>> Get(int id)
        {
            var request = new Application.UseCases.Categories.GetCategory.GetCategoryRequest(id);
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Application.UseCases.Categories.CreateCategory.CreateCategoryRequest request)
        {
            var result = await _mediator.Send(request);
            return CreatedAtRoute(null, new { Id = result });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Application.UseCases.Categories.UpdateCategory.UpdateCategoryRequest request)
        {
            request.CategoryId = id;
            var result = await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var request = new Application.UseCases.Categories.DeleteCategory.DeleteCategoryRequest(id);
            var result = await _mediator.Send(request);
            return Ok();
        }
    }
}