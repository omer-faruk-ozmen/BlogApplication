using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.Post.DeleteLikes
{
    public class DeletePostLikesCommandRequest : IRequest<bool>
    {
        public DeletePostLikesCommandRequest(Guid postId, Guid userId)
        {
            PostId = postId;
            UserId = userId;
        }

        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
