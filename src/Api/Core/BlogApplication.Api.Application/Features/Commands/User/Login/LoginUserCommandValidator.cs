using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models.RequestModels.User;
using FluentValidation;

namespace BlogApplication.Api.Application.Features.Commands.User.Login
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommandRequest>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(i => i.EmailAddress)
                .NotNull()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("{PropertyName} not a valid email address ");

            RuleFor(i => i.Password)
                .NotNull()
                .MinimumLength(8)
                .WithMessage("{PropertyName} should at least be {MinLength} characters");
        }
    }
}
