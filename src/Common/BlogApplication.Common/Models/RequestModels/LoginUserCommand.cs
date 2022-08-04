﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models.Queries;
using MediatR;

namespace BlogApplication.Common.Models.RequestModels
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }


        public LoginUserCommand(string emailAddress, string password)
        {
            EmailAddress = emailAddress;
            Password = password;
        }

        public LoginUserCommand()
        {

        }
    }
}
