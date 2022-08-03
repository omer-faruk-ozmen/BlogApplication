using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Api.Domain.Models
{
    public class PostCommentLikes : BaseEntity
    {
        public Guid PostCommentId { get; set; }
        public Guid CreatedById { get; set; }

        public virtual PostComment PostComment { get; set; }
        public virtual User CreatedUser { get; set; }
    }
}
