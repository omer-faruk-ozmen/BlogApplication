﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models;

namespace BlogApplication.Common.Events.Post
{
    public class CreatePostLikesEvent
    {
        public Guid PostId { get; set; }
        public LikedStatus LikedStatus { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
