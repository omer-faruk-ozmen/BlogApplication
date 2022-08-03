using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using BlogApplication.Infrastructure.Persistence.Context;

namespace BlogApplication.Infrastructure.Persistence.Repositories.Post;

public class PostReadRepository : ReadRepository<Api.Domain.Models.Post>, IPostReadRepository
{
    public PostReadRepository(BlogApplicationContext context) : base(context)
    {
    }
}