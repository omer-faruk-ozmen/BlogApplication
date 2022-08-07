using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.PostComment.DeleteLikes
{
    public class DeletePostCommentLikesCommandRequest : IRequest<bool>
    {
        public DeletePostCommentLikesCommandRequest(Guid postCommentId, Guid userId)
        {
            PostCommentId = postCommentId;
            UserId = userId;
        }



        public Guid PostCommentId { get; set; }
        public Guid UserId { get; set; }
    }
}
