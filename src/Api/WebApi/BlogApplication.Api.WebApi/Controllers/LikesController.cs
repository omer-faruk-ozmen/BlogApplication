using BlogApplication.Api.Application.Features.Commands.Post.DeleteLikes;
using BlogApplication.Common.Models;
using BlogApplication.Common.Models.RequestModels.Post;
using BlogApplication.Common.Models.RequestModels.PostComment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LikesController : BaseController
    {
        private readonly IMediator _mediator;

        public LikesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("post/{postid}")]
        public async Task<IActionResult> CreatePostLiked(Guid postId, LikedStatus likedStatus = LikedStatus.Like)
        {
            var result = await _mediator.Send(new CreatePostLikesCommandRequest(postId, likedStatus, UserId.Value));

            return Ok(result);
        }

        [HttpGet]
        [Route("postcomment/{postcommentid}")]
        public async Task<IActionResult> CreatePostCommentLiked(Guid postCommentId, LikedStatus likedStatus = LikedStatus.Like)
        {
            var result = await _mediator.Send(new CreatePostCommentLikesCommandRequest(UserId.Value, likedStatus, postCommentId));

            return Ok(result);
        }


        [HttpGet]
        [Route("deletepostliked/{postid}")]
        public async Task<IActionResult> DeletePostLiked(Guid postId)
        {
            var result = await _mediator.Send(new DeletePostLikesCommandRequest(postId, UserId.Value));

            return Ok(result);
        }

        [HttpGet]
        [Route("deletepostcommentliked/{postcommentid}")]
        public async Task<IActionResult> DeletePostCommentLiked(Guid postCommentId)
        {
            var result = await _mediator.Send(new DeletePostLikesCommandRequest(postCommentId, UserId.Value));

            return Ok(result);
        }
    }
}
