using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Api.Application.Interfaces.Repositories.PostComment;

public interface IPostCommentReadRepository : IReadRepository<Domain.Models.PostComment>
{
}