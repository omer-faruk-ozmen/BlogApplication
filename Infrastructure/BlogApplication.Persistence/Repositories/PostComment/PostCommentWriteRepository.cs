using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Application.Repositories;
using BlogApplication.Application.Repositories.PostComment;
using BlogApplication.Persistence.Contexts;

namespace BlogApplication.Persistence.Repositories.PostComment
{
    public class PostCommentWriteRepository : WriteRepository<Domain.Entities.PostComment>, IPostCommentWriteRepository

    {
        public PostCommentWriteRepository(BlogAppDbContext context) : base(context)
        {
        }
    }
}
