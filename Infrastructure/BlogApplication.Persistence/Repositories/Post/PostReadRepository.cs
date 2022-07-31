using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Application.Repositories.Post;
using BlogApplication.Persistence.Contexts;

namespace BlogApplication.Persistence.Repositories.Post
{
    public class PostReadRepository : ReadRepository<Domain.Entities.Post>, IPostReadRepository
    {
        public PostReadRepository(BlogAppDbContext context) : base(context)
        {
        }
    }
}
