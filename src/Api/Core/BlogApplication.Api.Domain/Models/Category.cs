using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Api.Domain.Models;

public class Category : BaseEntity
{
    public string Name { get; set; }
    //public ICollection<Post> Posts { get; set; }
    public virtual ICollection<PostCategory> PostCategories { get; set; }
}