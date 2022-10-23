using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models.Page;
using BlogApplication.Common.Models.Queries;
using MediatR;

namespace BlogApplication.Api.Application.Features.Queries.GetPosts
{
    public class GetPostsQueryRequest : BasePagedQuery, IRequest<PageViewModel<GetPostsViewModel>>
    {
        public GetPostsQueryRequest(Guid? userId, int page, int pageSize) : base(page, pageSize)
        {
            UserId = userId;
        }

        public Guid? UserId { get; set; }

    }
}
