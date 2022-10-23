using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using BlogApplication.Api.Domain.Models;
using BlogApplication.Common.Infrastructure.Extensions;
using BlogApplication.Common.Models;
using BlogApplication.Common.Models.Page;
using BlogApplication.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostCategory = BlogApplication.Common.Models.Queries.PostCategory;
using PostTag = BlogApplication.Common.Models.Queries.PostTag;

namespace BlogApplication.Api.Application.Features.Queries.GetTagPosts
{
    public class GetTagPostsQueryHandler : IRequestHandler<GetTagPostsQueryRequest, PageViewModel<GetTagPostsViewModel>>
    {
        private readonly IPostReadRepository _postReadRepository;

        public GetTagPostsQueryHandler(IPostReadRepository postReadRepository)
        {
            _postReadRepository = postReadRepository;
        }

        public async Task<PageViewModel<GetTagPostsViewModel>> Handle(GetTagPostsQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _postReadRepository
                .AsQueryable()
                .Where(i => i.Published == true);

            query = query.Include(i => i.PostTags)
                .ThenInclude(i => i.Tag)
                .Where(i => i.PostTags.Any(a => a.TagId == request.TagId));

            var list = query.Select(i => new GetTagPostsViewModel()
            {
                Id = i.Id,
                Summary = i.Summary,
                Title = i.Title,
                CreateDate = i.CreateDate,
                Published = i.Published,
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
                PostCategories = i.PostCategories.Select(p => new PostCategory()
                {
                    CategoryName = p.Category.Name
                }).ToList()

            }).OrderByDescending(i => i.CreateDate);

            var posts = await list.GetPaged(request.Page, request.PageSize);

            return posts;
        }
    }
}
