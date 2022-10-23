using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models.Page;
using BlogApplication.Common.Models.Queries;
using MediatR;

namespace BlogApplication.Api.Application.Features.Queries.GetCategoryPosts
{
    public class GetCategoryPostsQueryRequest : BasePagedQuery, IRequest<PageViewModel<GetCategoryPostsViewModel>>
    {
        public GetCategoryPostsQueryRequest(int page, int pageSize, Guid categoryId, Guid? userId) : base(page, pageSize)
        {
            CategoryId = categoryId;
            UserId = userId;
        }

        public Guid CategoryId { get; set; }
        public Guid? UserId { get; set; }
    }
}
