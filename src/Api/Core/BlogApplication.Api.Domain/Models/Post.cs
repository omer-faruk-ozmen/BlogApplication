using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Api.Domain.Models;

public class Post : BaseEntity
{
    public string Title { get; set; }
    public string MetaTitle { get; set; }
    public string Summary { get; set; }
    public string Content { get; set; }
    public bool Published { get; set; }
    public Guid CreatedById { get; set; }
    public virtual User CreatedBy { get; set; }
    public virtual ICollection<PostComment> PostComments { get; set; }
    public virtual ICollection<PostFavorite> PostFavorites  { get; set; }
    public virtual ICollection<Tag> Tags { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
}