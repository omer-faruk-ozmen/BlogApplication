using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models;

namespace BlogApplication.Common.Events.PostComment
{
    public class CreatePostCommentLikesEvent
    {
        public Guid PostCommentId { get; set; }
        public LikedStatus LikedStatus { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
