using BlogApplication.Api.Application.Interfaces.Repositories.EmailConfirmation;
using BlogApplication.Infrastructure.Persistence.Context;

namespace BlogApplication.Infrastructure.Persistence.Repositories.EmailConfirmation;

public class EmailConfirmationReadRepository : ReadRepository<Api.Domain.Models.EmailConfirmation>, IEmailConfirmationReadRepository
{
    public EmailConfirmationReadRepository(BlogApplicationContext context) : base(context)
    {
    }
}