using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.EmailConfirmation;
using BlogApplication.Infrastructure.Persistence.Context;

namespace BlogApplication.Infrastructure.Persistence.Repositories.EmailConfirmation;

public class EmailConfirmationWriteRepository : WriteRepository<Api.Domain.Models.EmailConfirmation>, IEmailConfirmationWriteRepository
{
    public EmailConfirmationWriteRepository(BlogApplicationContext context) : base(context)
    {
    }
}