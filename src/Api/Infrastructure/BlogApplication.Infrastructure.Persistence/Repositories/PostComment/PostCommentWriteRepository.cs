using BlogApplication.Api.Application.Interfaces.Repositories.PostComment;
using BlogApplication.Infrastructure.Persistence.Context;

namespace BlogApplication.Infrastructure.Persistence.Repositories.PostComment;

public class PostCommentWriteRepository : WriteRepository<Api.Domain.Models.PostComment>, IPostCommentWriteRepository
{
    public PostCommentWriteRepository(BlogApplicationContext context) : base(context)
    {
    }
}