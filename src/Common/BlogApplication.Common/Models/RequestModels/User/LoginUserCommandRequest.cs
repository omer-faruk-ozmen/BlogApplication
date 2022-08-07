using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models.Queries;
using MediatR;

namespace BlogApplication.Common.Models.RequestModels.User
{
    public class LoginUserCommandRequest : IRequest<LoginUserViewModel>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }


        public LoginUserCommandRequest(string emailAddress, string password)
        {
            EmailAddress = emailAddress;
            Password = password;
        }

        public LoginUserCommandRequest()
        {

        }
    }
}
