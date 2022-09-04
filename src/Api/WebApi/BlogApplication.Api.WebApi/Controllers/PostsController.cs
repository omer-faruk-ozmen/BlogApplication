using BlogApplication.Api.Application.Features.Queries.GetPostComments;
using BlogApplication.Api.Application.Features.Queries.GetPostDetail;
using BlogApplication.Api.Application.Features.Queries.GetPosts;
using BlogApplication.Api.Application.Features.Queries.GetUserPosts;
using BlogApplication.Common.Models.Queries;
using BlogApplication.Common.Models.RequestModels.Post;
using BlogApplication.Common.Models.RequestModels.PostComment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace BlogApplication.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : BaseController
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts(int page, int pageSize)
        {
            var posts = await _mediator.Send(new GetPostsQueryRequest(UserId, page, pageSize));
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetPostDetailQueryRequest(postId: id, userId: UserId));
            return Ok(result);
        }

        [HttpGet]
        [Route("UserPosts")]
        public async Task<IActionResult> GetUserPosts(string? userName, Guid userId, int page, int pageSize)
        {
            if (userId == Guid.Empty && string.IsNullOrEmpty(userName))
                userId = UserId.Value;

            var result = await _mediator.Send(new GetUserPostsQueryRequest(userId, userName, page, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("Comments/{id}")]
        public async Task<IActionResult> GetPostComments(Guid id, int page, int pageSize)
        {
            var result = await _mediator.Send(new GetPostCommentsQueryRequest(id, UserId, page, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchPostQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreatePost")]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommandRequest command)
        {
            if (!command.CreatedById.HasValue)
                command.CreatedById = UserId;

            var result = await _mediator.Send(command);

            return Ok(result);


        }
        [HttpPost]
        [Route("CreatePostComment")]
        [Authorize]
        public async Task<IActionResult> CreatePostComment([FromBody] CreatePostCommentCommandRequest command)
        {
            if (!command.CreatedById.HasValue)
                command.CreatedById = UserId;

            var result = await _mediator.Send(command);

            return Ok(result);

        }
    }
}
