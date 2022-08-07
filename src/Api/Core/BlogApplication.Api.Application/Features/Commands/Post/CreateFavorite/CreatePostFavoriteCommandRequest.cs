using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.Post.CreateFavorite
{
    public class CreatePostFavoriteCommandRequest : IRequest<bool>
    {
        public CreatePostFavoriteCommandRequest(Guid? userId, Guid? postId)
        {
            UserId = userId;
            PostId = postId;
        }

        public Guid? PostId { get; set; }
        public Guid? UserId { get; set; }
    }
}
