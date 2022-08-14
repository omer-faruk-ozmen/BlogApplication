using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models.Page;
using BlogApplication.Common.Models.Queries;
using MediatR;

namespace BlogApplication.Api.Application.Features.Queries.GetPostComments
{
    public class GetPostCommentsQueryRequest : BasePagedQuery, IRequest<PageViewModel<GetPostCommentsViewModel>>
    {
        public GetPostCommentsQueryRequest(Guid postId, Guid? userId, int page, int pageSize) : base(page, pageSize)
        {
            PostId = postId;
            UserId = userId;
        }

        public Guid PostId { get; set; }
        public Guid? UserId { get; set; }
    }
}
