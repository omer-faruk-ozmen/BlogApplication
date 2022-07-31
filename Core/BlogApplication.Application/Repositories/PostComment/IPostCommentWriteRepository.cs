using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Application.Repositories.PostComment
{
    public interface IPostCommentWriteRepository : IWriteRepository<Domain.Entities.PostComment>
    {
    }
}
