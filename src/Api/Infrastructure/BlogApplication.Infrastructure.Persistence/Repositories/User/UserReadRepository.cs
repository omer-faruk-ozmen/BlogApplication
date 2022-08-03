using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.User;
using BlogApplication.Infrastructure.Persistence.Context;

namespace BlogApplication.Infrastructure.Persistence.Repositories.User
{
    public class UserReadRepository : ReadRepository<Api.Domain.Models.User>, IUserReadRepository
    {
        public UserReadRepository(BlogApplicationContext context) : base(context)
        {
        }
    }
}
