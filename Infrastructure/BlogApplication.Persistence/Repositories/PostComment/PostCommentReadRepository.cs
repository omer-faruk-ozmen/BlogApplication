﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Application.Repositories.PostComment;
using BlogApplication.Persistence.Contexts;

namespace BlogApplication.Persistence.Repositories.PostComment
{
    public class PostCommentReadRepository : ReadRepository<Domain.Entities.PostComment>, IPostCommentReadRepository
    {
        public PostCommentReadRepository(BlogAppDbContext context) : base(context)
        {
        }
    }
}
