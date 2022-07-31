using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Application.Repositories.Category;
using BlogApplication.Persistence.Contexts;

namespace BlogApplication.Persistence.Repositories.Category
{
    public class CategoryWriteRepository : WriteRepository<Domain.Entities.Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(BlogAppDbContext context) : base(context)
        {
        }
    }
}
