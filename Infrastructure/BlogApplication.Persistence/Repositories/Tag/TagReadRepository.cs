using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Application.Repositories.Tag;
using BlogApplication.Persistence.Contexts;

namespace BlogApplication.Persistence.Repositories.Tag
{
    public class TagReadRepository : ReadRepository<Domain.Entities.Tag>, ITagReadRepository
    {
        public TagReadRepository(BlogAppDbContext context) : base(context)
        {
        }
    }
}
