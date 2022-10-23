using System.Diagnostics.CodeAnalysis;
using BlogApplication.Api.Application.Features.Queries.GetTags;
using BlogApplication.Common.Models.RequestModels.Category;
using BlogApplication.Common.Models.RequestModels.Tag;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : BaseController
    {
        private readonly IMediator _mediator;

        public TagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var result = await _mediator.Send(new GetTagsQueryRequest());
            return Ok(result);
        }


        [HttpPost]
        [Route("CreateTag")]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagCommandRequest command)
        {
            var result = await _mediator.Send(command);
            return Created("", result);
        }


        [HttpDelete]
        [Route("DeleteTag")]
        public async Task<IActionResult> DeleteTag([FromBody] DeleteTagCommandRequest command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
