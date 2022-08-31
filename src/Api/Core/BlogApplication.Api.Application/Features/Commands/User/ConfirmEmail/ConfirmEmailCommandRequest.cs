using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.User.ConfirmEmail
{
    public class ConfirmEmailCommandRequest : IRequest<bool>
    {
        public Guid ConfirmationId { get; set; }
    }
}
