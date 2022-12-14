using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogApplication.Common.Models.RequestModels.Post
{
    public class CreatePostLikesCommandRequest : IRequest<bool>
    {
        public CreatePostLikesCommandRequest(Guid postId, LikedStatus likedStatus, Guid createdBy)
        {
            PostId = postId;
            LikedStatus = likedStatus;
            CreatedBy = createdBy;
        }

        public Guid PostId { get; set; }
        public LikedStatus LikedStatus { get; set; }
        public Guid CreatedBy { get; set; }
        
    }
}
