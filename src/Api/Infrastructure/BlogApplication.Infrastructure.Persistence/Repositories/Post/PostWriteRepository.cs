using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using BlogApplication.Infrastructure.Persistence.Context;

namespace BlogApplication.Infrastructure.Persistence.Repositories.Post;

public class PostWriteRepository : WriteRepository<Api.Domain.Models.Post>, IPostWriteRepository
{
    public PostWriteRepository(BlogApplicationContext context) : base(context)
    {
    }
}