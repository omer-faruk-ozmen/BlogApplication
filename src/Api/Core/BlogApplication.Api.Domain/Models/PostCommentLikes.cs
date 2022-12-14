using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models;

namespace BlogApplication.Api.Domain.Models
{
    public class PostCommentLikes : BaseEntity
    {
        public Guid PostCommentId { get; set; }
        public LikedStatus LikedStatus { get; set; }
        public Guid CreatedById { get; set; }

        public virtual PostComment PostComment { get; set; }
    }
}
