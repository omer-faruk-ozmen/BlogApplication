using BlogApplication.Api.Application.Interfaces.Repositories.Tag;
using BlogApplication.Infrastructure.Persistence.Context;

namespace BlogApplication.Infrastructure.Persistence.Repositories.Tag;

public class TagReadRepository : ReadRepository<Api.Domain.Models.Tag>, ITagReadRepository
{
    public TagReadRepository(BlogApplicationContext context) : base(context)
    {
    }
}