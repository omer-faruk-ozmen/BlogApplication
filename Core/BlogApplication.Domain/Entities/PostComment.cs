using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Domain.Entities.Common;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApplication.Domain.Entities
{
    public class PostComment : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public ICollection<Post>? Posts { get; set; }

    }
}
