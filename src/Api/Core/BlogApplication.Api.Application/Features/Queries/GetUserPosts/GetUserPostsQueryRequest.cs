﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models.Page;
using BlogApplication.Common.Models.Queries;
using MediatR;

namespace BlogApplication.Api.Application.Features.Queries.GetUserPosts
{
    public class GetUserPostsQueryRequest : BasePagedQuery, IRequest<PageViewModel<GetUserPostsViewModel>>
    {
        public GetUserPostsQueryRequest(Guid? userId, string userName = null, int page = 1, int pageSize = 10) : base(page, pageSize)
        {
            UserId = userId;
            UserName = userName;
        }


        public Guid? UserId { get; set; }
        public string UserName { get; set; }



    }
}
