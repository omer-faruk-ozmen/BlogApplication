using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Tag;
using BlogApplication.Common.Models.Queries;
using MediatR;

namespace BlogApplication.Api.Application.Features.Queries.GetCategories
{
    public class GetCategoriesQueryRequest : IRequest<List<GetCategoriesViewModel>>
    {
    }
}
