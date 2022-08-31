using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.User;
using BlogApplication.Common.Events.User;
using BlogApplication.Common.Infrastructure;
using BlogApplication.Common.Infrastructure.Exceptions;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.User.ChangePassword
{
    public class UserChangePasswordCommandHandler : IRequestHandler<UserPasswordChangedCommandRequest, bool>
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;

        public UserChangePasswordCommandHandler(IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }

        public async Task<bool> Handle(UserPasswordChangedCommandRequest request, CancellationToken cancellationToken)
        {
            if (!request.UserId.HasValue)
            {
                throw new ArgumentException(nameof(request.UserId));
            }

            var dbUser = await _userReadRepository.GetByIdAsync(request.UserId.Value);

            if (dbUser is null)
                throw new DatabaseValidationException("User not found!");

            var encryptPass = PasswordEncryptor.Encrypt(request.OldPassword);
            var newEncPass = PasswordEncryptor.Encrypt(request.NewPassword);


            if (dbUser.Password != encryptPass)
                throw new DatabaseValidationException("Old password wrong!");

            dbUser.Password = newEncPass;

            await _userWriteRepository.UpdateAsync(dbUser);

            return true;
        }
    }
}
