using BlogApplication.Api.Application.Features.Queries.GetCategories;
using BlogApplication.Api.Application.Features.Queries.GetTags;
using BlogApplication.Common.Models.RequestModels.Category;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _mediator.Send(new GetCategoriesQueryRequest());
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommandRequest command)
        {
            var result = await _mediator.Send(command);
            return Created("", result);
        }

        [HttpDelete]
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryCommandRequest command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
