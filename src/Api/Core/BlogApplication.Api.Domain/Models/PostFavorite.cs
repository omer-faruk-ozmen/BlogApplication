using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Api.Domain.Models;

public class PostFavorite : BaseEntity
{
    public Guid PostId { get; set; }
    public Guid CreatedById { get; set; }

    public virtual User CreatedBy { get; set; }
    public virtual Post Post { get; set; }
}