using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Tag;
using BlogApplication.Infrastructure.Persistence.Context;

namespace BlogApplication.Infrastructure.Persistence.Repositories.Tag;

public class TagWriteRepository : WriteRepository<Api.Domain.Models.Tag>, ITagWriteRepository
{
    public TagWriteRepository(BlogApplicationContext context) : base(context)
    {
    }
}