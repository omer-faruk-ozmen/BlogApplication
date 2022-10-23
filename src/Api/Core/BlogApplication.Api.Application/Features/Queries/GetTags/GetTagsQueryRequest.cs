using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models.Queries;
using MediatR;

namespace BlogApplication.Api.Application.Features.Queries.GetTags
{
    public class GetTagsQueryRequest : IRequest<List<GetTagsViewModel>>
    {
    }
}
