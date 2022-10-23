using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Api.Domain.Models;

public class Tag : BaseEntity
{
    public string Name { get; set; }
    public ICollection<PostTag> PostTags { get; set; }
}