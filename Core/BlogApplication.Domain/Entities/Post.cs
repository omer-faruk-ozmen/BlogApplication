using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Domain.Entities.Common;
using BlogApplication.Domain.Entities.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApplication.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string? Title { get; set; }
        public string? MetaTitle { get; set; }
        public string? Summary { get; set; }
        public bool Published { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? Content { get; set; }
        public ICollection<AppUser>? AppUsers { get; set; }
        public PostComment? PostComment { get; set; }
        public PostMeta? PostMeta { get; set; }
        public PostTag? PostTag { get; set; }
    }
}
