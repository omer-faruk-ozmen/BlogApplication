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
        public AppUser? AppUser { get; set; }
        public ICollection<PostComment>? PostComments { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public ICollection<PostMeta>? PostMetas { get; set; }
        public ICollection<Tag>? Tags { get; set; }

        

    }
}
