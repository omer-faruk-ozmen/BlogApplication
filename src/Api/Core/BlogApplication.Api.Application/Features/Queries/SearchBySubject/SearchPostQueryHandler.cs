using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using BlogApplication.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Api.Application.Features.Queries.SearchBySubject
{
    public class SearchPostQueryHandler : IRequestHandler<SearchPostQueryRequest, List<SearchPostViewModel>>
    {
        private readonly IPostReadRepository _postReadRepository;

        public SearchPostQueryHandler(IPostReadRepository postReadRepository)
        {
            _postReadRepository = postReadRepository;
        }

        public async Task<List<SearchPostViewModel>> Handle(SearchPostQueryRequest request, CancellationToken cancellationToken)
        {
            // TODO validation, request.SearchText length should be checked

            var result = _postReadRepository
                .AsQueryable()
                //.Get(i => EF.Functions.Like(i.Summary, $"{request.SearchText}%"))
                .Select(i => new SearchPostViewModel()
                {
                    Id = i.Id,
                    Summary = i.Summary
                })
                .Where(s => s.Summary!.Contains(request.SearchText));

            return await result.ToListAsync(cancellationToken);
        }
    }
}
