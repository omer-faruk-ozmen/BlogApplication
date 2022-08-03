using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Api.Domain.Models
{
    public class PostComment : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CreatedById { get; set; }
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual ICollection<PostCommentLikes> PostCommentLikeds { get; set; }

    }
}
