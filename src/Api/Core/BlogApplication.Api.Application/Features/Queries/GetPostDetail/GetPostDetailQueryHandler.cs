using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using BlogApplication.Common.Infrastructure.Exceptions;
using BlogApplication.Common.Models;
using BlogApplication.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Api.Application.Features.Queries.GetPostDetail
{
    public class GetPostDetailQueryHandler : IRequestHandler<GetPostDetailQueryRequest, GetPostDetailViewModel>
    {
        private readonly IPostReadRepository _postReadRepository;

        public GetPostDetailQueryHandler(IPostReadRepository postReadRepository)
        {
            _postReadRepository = postReadRepository;
        }

        public async Task<GetPostDetailViewModel> Handle(GetPostDetailQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _postReadRepository.AsQueryable();

            query = query
                .Include(i => i.PostFavorites)
                .Include(i => i.CreatedBy)
                .Include(i => i.PostLikes)
                .Include(i => i.PostTags)
                .Where(i => i.Id == request.PostId);

            var list = query.Select(i => new GetPostDetailViewModel()
            {
                Id = i.Id,
                Title = i.Title,
                MetaTitle = i.MetaTitle,
                Summary = i.Summary,
                Content = i.Content,
                CreateDate = i.CreateDate,
                IsFavorite = request.UserId.HasValue && i.PostFavorites.Any(j => j.CreatedById == request.UserId),
                CreateByUserName = i.CreatedBy.UserName,
                Published = i.Published,
                LikedStatus = request.UserId.HasValue && i.PostLikes.Any(j => j.CreatedById == request.UserId) ? i.PostLikes.FirstOrDefault(j => j.CreatedById == request.UserId)!.LikedStatus : LikedStatus.None,
                FavoriteCount = i.PostFavorites.Count,
                UpdatedDate = i.UpdatedDate,
                PostCategories = i.PostCategories.Select(p => new PostCategory()
                {
                    CategoryName = p.Category.Name
                }).ToList(),
                PostTags = i.PostTags.Select(p=>new PostTag()
                {
                    TagName = p.Tag.Name
                }).ToList()


            });

            return await list.FirstOrDefaultAsync(cancellationToken) ?? throw new DatabaseValidationException("An Unexpected Error Occured");
        }
    }
}
