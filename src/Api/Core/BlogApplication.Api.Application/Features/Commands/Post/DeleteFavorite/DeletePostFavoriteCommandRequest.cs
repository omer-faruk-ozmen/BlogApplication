using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.Post.DeleteFavorite
{
    public class DeletePostFavoriteCommandRequest : IRequest<bool>
    {
        public DeletePostFavoriteCommandRequest(Guid userId, Guid postId)
        {
            UserId = userId;
            PostId = postId;
        }

        public Guid PostId { get; set; }
        public Guid UserId { get; set; }

    }
}
