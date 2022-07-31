using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Domain.Entities.Common;

namespace BlogApplication.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string? Title { get; set; }
        public string? MetaTitle { get; set; }
        public string? Content { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
