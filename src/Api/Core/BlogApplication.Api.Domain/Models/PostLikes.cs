using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.ViewModels;

namespace BlogApplication.Api.Domain.Models;

public class PostLikes : BaseEntity
{
    public Guid PostId { get; set; }
    public LikedStatus LikedStatus { get; set; }
    public Guid CreatedById { get; set; }

    public virtual Post Post { get; set; }
}

