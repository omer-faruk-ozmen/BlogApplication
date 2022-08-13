using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using BlogApplication.Common.Infrastructure.Extensions;
using BlogApplication.Common.Models.Page;
using BlogApplication.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Api.Application.Features.Queries.GetUserPosts
{
    public class GetUserPostsQueryHandler : IRequestHandler<GetUserPostsQueryRequest, PageViewModel<GetUserPostsViewModel>>
    {
        private readonly IPostReadRepository _postReadRepository;

        public GetUserPostsQueryHandler(IPostReadRepository postReadRepository)
        {
            _postReadRepository = postReadRepository;
        }

        public async Task<PageViewModel<GetUserPostsViewModel>> Handle(GetUserPostsQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _postReadRepository.AsQueryable();

            if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
            {
                query = query.Where(i => i.CreatedById == request.UserId);
            }
            else if (!string.IsNullOrEmpty(request.UserName))
            {
                query = query.Where(i => i.CreatedBy.UserName == request.UserName);
            }
            else
                return null;

            query = query
                .Include(i => i.PostFavorites)
                .Include(i => i.CreatedBy);

            var list = query.Select(i => new GetUserPostsViewModel()
            {
                Id = i.Id,
                Title = i.Title,
                Summary = i.Summary,
                CreateDate = i.CreateDate,
                CreatedByUserName = i.CreatedBy.UserName,
                UpdatedDate = i.UpdatedDate,
                IsFavorite = false,
                FavoriteCount = i.PostFavorites.Count,

            });

            var posts = await list.GetPaged(request.Page, request.PageSize);

            return posts;
        }
    }
}
