using BlogApplication.Api.Application.Interfaces.Repositories.Category;
using BlogApplication.Infrastructure.Persistence.Context;

namespace BlogApplication.Infrastructure.Persistence.Repositories.Category;

public class CategoryWriteRepository : WriteRepository<Api.Domain.Models.Category>, ICategoryWriteRepository
{
    public CategoryWriteRepository(BlogApplicationContext context) : base(context)
    {
    }
}