using BlogApplication.Api.Application.Features.Commands.Post.CreateFavorite;
using BlogApplication.Api.Application.Features.Queries.GetUserFavorites;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteController : BaseController
    {

        private readonly IMediator _mediator;

        public FavoriteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("UserFavorites")]
        public async Task<IActionResult> GetUserFavorites(int page, int pageSize)
        {


            var result = await _mediator.Send(new GetUserFavoritesQueryRequest(page, pageSize, UserId.Value));

            return Ok(result);
        }

        [HttpPost]
        [Route("post/{postId}")]
        public async Task<IActionResult> CreatePostFav(Guid postId)
        {
            var result = await _mediator.Send(new CreatePostFavoriteCommandRequest(UserId, postId));

            return Ok(result);
        }

        [HttpPost]
        [Route("postcomment/{postcommentid}")]
        public async Task<IActionResult> CreatePostCommentFav(Guid postCommentId)
        {
            var result = await _mediator.Send(new CreatePostFavoriteCommandRequest(UserId.Value, postCommentId));

            return Ok(result);
        }

        [HttpPost]
        [Route("deletepostfav/{postId}")]
        public async Task<IActionResult> DeletePostFav(Guid postId)
        {
            var result = await _mediator.Send(new CreatePostFavoriteCommandRequest(UserId.Value, postId));

            return Ok(result);
        }

        [HttpPost]
        [Route("deletepostcommentfav/{postcommentid}")]
        public async Task<IActionResult> DeletePostCommentFav(Guid postCommentId)
        {
            var result = await _mediator.Send(new CreatePostFavoriteCommandRequest(UserId.Value, postCommentId));

            return Ok(result);
        }
    }
}
