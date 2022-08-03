using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.PostComment;
using BlogApplication.Infrastructure.Persistence.Context;

namespace BlogApplication.Infrastructure.Persistence.Repositories.PostComment;

public class PostCommentReadRepository : ReadRepository<Api.Domain.Models.PostComment>, IPostCommentReadRepository
{
    public PostCommentReadRepository(BlogApplicationContext context) : base(context)
    {
    }
}