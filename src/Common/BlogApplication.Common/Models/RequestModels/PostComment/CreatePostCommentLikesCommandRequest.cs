using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogApplication.Common.Models.RequestModels.PostComment
{
    public class CreatePostCommentLikesCommandRequest : IRequest<bool>
    {
        public CreatePostCommentLikesCommandRequest(Guid createdBy, LikedStatus likedStatus, Guid postCommentId)
        {
            CreatedBy = createdBy;
            LikedStatus = likedStatus;
            PostCommentId = postCommentId;
        }

        public CreatePostCommentLikesCommandRequest()
        {

        }

        public Guid PostCommentId { get; set; }
        public LikedStatus LikedStatus { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
