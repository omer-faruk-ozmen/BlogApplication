using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Category;
using BlogApplication.Infrastructure.Persistence.Context;

namespace BlogApplication.Infrastructure.Persistence.Repositories.Category;

public class CategoryReadRepository : ReadRepository<Api.Domain.Models.Category>, ICategoryReadRepository
{
    public CategoryReadRepository(BlogApplicationContext context) : base(context)
    {
    }
}