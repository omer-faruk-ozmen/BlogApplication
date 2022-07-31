using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Application.Repositories.PostMeta;
using BlogApplication.Persistence.Contexts;

namespace BlogApplication.Persistence.Repositories.PostMeta
{
    public class PostMetaWriteRepository : WriteRepository<Domain.Entities.PostMeta>, IPostMetaWriteRepository
    {
        public PostMetaWriteRepository(BlogAppDbContext context) : base(context)
        {
        }
    }
}
