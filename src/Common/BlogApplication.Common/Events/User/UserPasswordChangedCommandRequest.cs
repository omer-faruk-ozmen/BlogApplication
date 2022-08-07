using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogApplication.Common.Events.User
{
    public class UserPasswordChangedCommandRequest : IRequest<bool>
    {
        public UserPasswordChangedCommandRequest(Guid? userId, string oldPassword, string newPassword)
        {
            UserId = userId;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public Guid? UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
