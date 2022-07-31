using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Domain.Entities
{
    public class PostCategory
    {
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
