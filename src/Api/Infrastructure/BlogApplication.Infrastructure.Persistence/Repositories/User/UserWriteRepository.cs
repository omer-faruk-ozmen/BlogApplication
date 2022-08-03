using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.User;
using BlogApplication.Infrastructure.Persistence.Context;

namespace BlogApplication.Infrastructure.Persistence.Repositories.User
{
    public class UserWriteRepository : WriteRepository<Api.Domain.Models.User>, IUserWriteRepository
    {
        public UserWriteRepository(BlogApplicationContext context) : base(context)
        {
        }
    }
}
