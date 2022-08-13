using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models.Queries;
using MediatR;

namespace BlogApplication.Api.Application.Features.Queries.GetPosts
{
    public class GetPostsQueryRequest : IRequest<List<GetPostsViewModel>>
    {
        public int Count { get; set; } = 15;
    }
}
