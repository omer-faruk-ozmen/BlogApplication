using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Domain.Entities
{
    public class PostMeta
    {
        public string? Key { get; set; }
        public string? Content { get; set; }    
        public ICollection<Post>? Posts { get; set; }
    }
}
