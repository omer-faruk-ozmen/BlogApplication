using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models.Queries;
using MediatR;

namespace BlogApplication.Api.Application.Features.Queries.GetPostDetail
{
    public class GetPostDetailQueryRequest : IRequest<GetPostDetailViewModel>
    {
        public GetPostDetailQueryRequest(Guid? userId, Guid postId)
        {
            UserId = userId;
            PostId = postId;
        }

        public Guid PostId { get; set; }
        public Guid? UserId { get; set; }
    }
}
