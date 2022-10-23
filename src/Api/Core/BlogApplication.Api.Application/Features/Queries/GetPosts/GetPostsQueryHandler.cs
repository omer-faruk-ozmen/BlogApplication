using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using BlogApplication.Common.Infrastructure.Exceptions;
using BlogApplication.Common.Infrastructure.Extensions;
using BlogApplication.Common.Models;
using BlogApplication.Common.Models.Page;
using BlogApplication.Common.Models.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogApplication.Api.Application.Features.Queries.GetPosts
{
    public class GetPostsQueryHandler : IRequestHandler<GetPostsQueryRequest, PageViewModel<GetPostsViewModel>>
    {
        private readonly IPostReadRepository _postReadRepository;
        private readonly ILogger<GetPostsQueryHandler> _logger;


        public GetPostsQueryHandler(IPostReadRepository postReadRepository, ILogger<GetPostsQueryHandler> logger)
        {
            _postReadRepository = postReadRepository;
            _logger = logger;
        }

        public async Task<PageViewModel<GetPostsViewModel>> Handle(GetPostsQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _postReadRepository
                .AsQueryable()
                .Where(i => i.Published == true)
                ;




            _logger.LogInformation("Inquiry has been made");

            query = query
                .Include(i => i.PostFavorites)
                .Include(i => i.CreatedBy)
                .Include(i => i.PostLikes);


            var list = query.Select(i => new GetPostsViewModel()
            {
                Id = i.Id,
                Summary = i.Summary,
                Title = i.Title,
                CreateDate = i.CreateDate,
                Published = i.Published,
                CreatedByUserName = i.CreatedBy.UserName,
                CommentCount = i.PostComments.Count,
                FavoriteCount = i.PostFavorites.Count,
                LikesCount = i.PostLikes.Count,
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
