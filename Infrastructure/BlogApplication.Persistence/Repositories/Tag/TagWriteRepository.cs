using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Application.Repositories.Tag;
using BlogApplication.Persistence.Contexts;

namespace BlogApplication.Persistence.Repositories.Tag
{
    public class TagWriteRepository : WriteRepository<Domain.Entities.Tag>, ITagWriteRepository
    {
        public TagWriteRepository(BlogAppDbContext context) : base(context)
        {
        }
    }
}
