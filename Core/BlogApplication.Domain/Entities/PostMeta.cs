using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Domain.Entities.Common;

namespace BlogApplication.Domain.Entities
{
    public class PostMeta : BaseEntity
    {
        public string? Key { get; set; }
        public string? Content { get; set; }
        public Post? Post { get; set; }
    }
}
