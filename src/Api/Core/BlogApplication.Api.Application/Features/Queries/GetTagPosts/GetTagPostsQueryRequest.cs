using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models.Page;
using BlogApplication.Common.Models.Queries;
using MediatR;

namespace BlogApplication.Api.Application.Features.Queries.GetTagPosts
{
    public class GetTagPostsQueryRequest : BasePagedQuery, IRequest<PageViewModel<GetTagPostsViewModel>>
    {
        public GetTagPostsQueryRequest(int page, int pageSize, Guid tagId, Guid? userId) : base(page, pageSize)
        {
            TagId = tagId;
            UserId = userId;
        }

        public Guid TagId { get; set; }
        public Guid? UserId { get; set; }
    }
}
