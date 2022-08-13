using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using BlogApplication.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Api.Application.Features.Queries.GetPosts
{
    public class GetPostsQueryHandler : IRequestHandler<GetPostsQueryRequest, List<GetPostsViewModel>>
    {
        private readonly IPostReadRepository _postReadRepository;
        private readonly IMapper _mapper;

        public GetPostsQueryHandler(IPostReadRepository postReadRepository, IMapper mapper)
        {
            _postReadRepository = postReadRepository;
            _mapper = mapper;
        }

        public async Task<List<GetPostsViewModel>> Handle(GetPostsQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _postReadRepository.AsQueryable();


            query = query.Include(i => i.PostComments)
                .Where(i => i.Published == true)
                .OrderBy(i => i.CreateDate)
                .Take(request.Count);

            return await query.ProjectTo<GetPostsViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

        }
    }
}
