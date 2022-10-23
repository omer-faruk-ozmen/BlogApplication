using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Api.Domain.Models
{
    public class PostCategory
    {
        public Post Post { get; set; }
        public Category Category { get; set; }
        public Guid PostId { get; set; }
        public Guid CategoryId { get; set; }

    }
}
