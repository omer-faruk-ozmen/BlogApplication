using BlogApplication.Api.Application.Interfaces.Repositories.EmailConfirmation;
using BlogApplication.Api.Application.Interfaces.Repositories.User;
using BlogApplication.Common.Infrastructure.Exceptions;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.User.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommandRequest, bool>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly IEmailConfirmationWriteRepository _emailConfirmationWriteRepository;
    private readonly IEmailConfirmationReadRepository _emailConfirmationReadRepository;

    public ConfirmEmailCommandHandler(IUserReadRepository userReadRepository, IEmailConfirmationWriteRepository emailConfirmationWriteRepository, IEmailConfirmationReadRepository emailConfirmationReadRepository, IUserWriteRepository userWriteRepository)
    {
        _userReadRepository = userReadRepository;
        _emailConfirmationWriteRepository = emailConfirmationWriteRepository;
        _emailConfirmationReadRepository = emailConfirmationReadRepository;
        _userWriteRepository = userWriteRepository;
    }

    public async Task<bool> Handle(ConfirmEmailCommandRequest request, CancellationToken cancellationToken)
    {
        var confirmation = await _emailConfirmationReadRepository.GetByIdAsync(request.ConfirmationId);

        if (confirmation is null)
            throw new DatabaseValidationException("Confirmation not found!");

        var dbUser = await _userReadRepository.GetSingleAsync(i => i.EmailAddress == confirmation.NewEmailAddress);

        if (dbUser is null)
            throw new DatabaseValidationException("User not found with this email!");

        if (dbUser.EmailConfirmed)
            throw new DatabaseValidationException("Email address is already confirmed!");

        dbUser.EmailConfirmed = true;

        await _userWriteRepository.UpdateAsync(dbUser);

        return true;

    }
}