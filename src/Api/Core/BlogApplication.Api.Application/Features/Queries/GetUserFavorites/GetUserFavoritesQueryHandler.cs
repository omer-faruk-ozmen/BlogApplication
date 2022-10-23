using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using BlogApplication.Api.Application.Interfaces.Repositories.User;
using BlogApplication.Common.Infrastructure.Extensions;
using BlogApplication.Common.Models;
using BlogApplication.Common.Models.Page;
using BlogApplication.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Api.Application.Features.Queries.GetUserFavorites
{
    public class GetUserFavoritesQueryHandler : IRequestHandler<GetUserFavoritesQueryRequest, PageViewModel<GetUserFavoritesViewModel>>
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IPostReadRepository _postReadRepository;


        public GetUserFavoritesQueryHandler(IPostReadRepository postReadRepository, IUserReadRepository userReadRepository)
        {
            _postReadRepository = postReadRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task<PageViewModel<GetUserFavoritesViewModel>> Handle(GetUserFavoritesQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _postReadRepository.AsQueryable();

            var userQuery = _userReadRepository.AsQueryable();

            if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
                query = query.Where(i => i.CreatedById == request.UserId);

            else if (!string.IsNullOrEmpty(request.UserName))
                query = query.Where(i => i.CreatedBy.UserName == request.UserName);

            else
                return null;


            var list = query.Select(i => new GetUserFavoritesViewModel()
            {
                Id = i.Id,
                Summary = i.Summary,
                Title = i.Title,
                CreateDate = i.CreateDate,
                CommentCount = i.PostComments.Count,
                FavoriteCount = i.PostFavorites.Count,
                LikesCount = i.PostLikes.Count,
                CreatedByUserName = i.CreatedBy.UserName,
                IsFavorite = request.UserId.HasValue && i.PostFavorites.Any(j => j.CreatedById == request.UserId),
                LikedStatus = request.UserId.HasValue && i.PostLikes.Any(j => j.CreatedById == request.UserId) ? i.PostLikes.FirstOrDefault(j => j.CreatedById == request.UserId)!.LikedStatus : LikedStatus.None,
                PostTags = i.PostTags.Select(p => new PostTag()
                {
                    TagName = p.Tag.Name
                }).ToList(),
                PostCategories = i.PostCategories.Select(p=>new PostCategory()
                {
                    CategoryName = p.Category.Name
                }).ToList()
            });

            var posts = await list.GetPaged(request.Page, request.PageSize);

            return posts;
        }
    }
}
