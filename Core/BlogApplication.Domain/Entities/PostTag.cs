using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Domain.Entities
{
    public class PostTag
    {
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Tag>? Tags { get; set; }
    }
}
