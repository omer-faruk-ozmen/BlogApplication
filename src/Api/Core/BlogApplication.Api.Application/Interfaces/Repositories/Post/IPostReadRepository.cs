using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Api.Application.Interfaces.Repositories.Post;

public interface IPostReadRepository : IReadRepository<Domain.Models.Post>
{
}