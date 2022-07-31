using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Application.Repositories.Post
{
    public interface IPostReadRepository : IReadRepository<Domain.Entities.Post>
    {
    }
}
