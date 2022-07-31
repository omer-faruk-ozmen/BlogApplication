using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Application.Repositories.Post;
using BlogApplication.Persistence.Contexts;

namespace BlogApplication.Persistence.Repositories.Post
{
    public class PostWriteRepository : WriteRepository<Domain.Entities.Post>, IPostWriteRepository
    {
        public PostWriteRepository(BlogAppDbContext context) : base(context)
        {
        }
    }
}
