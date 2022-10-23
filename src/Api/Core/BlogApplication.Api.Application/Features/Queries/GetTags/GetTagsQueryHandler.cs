using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Tag;
using BlogApplication.Api.Domain.Models;
using BlogApplication.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Api.Application.Features.Queries.GetTags
{
    public class GetTagsQueryHandler : IRequestHandler<GetTagsQueryRequest, List<GetTagsViewModel> >
    {
        private readonly ITagReadRepository _tagReadRepository;

        public GetTagsQueryHandler(ITagReadRepository tagReadRepository)
        {
            _tagReadRepository = tagReadRepository;
        }

        public async Task<List<GetTagsViewModel>> Handle(GetTagsQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _tagReadRepository.AsQueryable();

            var list = await query.Select(i => new GetTagsViewModel()
            {
                Id = i.Id,
                Name = i.Name,
            }).ToListAsync();

            return list;
        }
    }
}
