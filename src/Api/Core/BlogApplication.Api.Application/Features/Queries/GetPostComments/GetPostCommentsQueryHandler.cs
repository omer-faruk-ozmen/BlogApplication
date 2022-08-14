using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using BlogApplication.Api.Application.Interfaces.Repositories.PostComment;
using BlogApplication.Common.Infrastructure.Extensions;
using BlogApplication.Common.Models;
using BlogApplication.Common.Models.Page;
using BlogApplication.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Api.Application.Features.Queries.GetPostComments
{
    public class GetPostCommentsQueryHandler : IRequestHandler<GetPostCommentsQueryRequest, PageViewModel<GetPostCommentsViewModel>>
    {
        private readonly IPostCommentReadRepository _postCommentReadRepository;


        public GetPostCommentsQueryHandler(IPostCommentReadRepository postCommentReadRepository)
        {
            _postCommentReadRepository = postCommentReadRepository;
        }

        public async Task<PageViewModel<GetPostCommentsViewModel>> Handle(GetPostCommentsQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _postCommentReadRepository.AsQueryable();

            query = query
                .Include(i => i.PostCommentLikeds)
                .Include(i => i.CreatedBy)
                .Where(i => i.PostId == request.PostId);

            var list = query.Select(i => new GetPostCommentsViewModel()
            {
                Id = i.Id,
                Title = i.Title,
                Content = i.Content,
                CreateDate = i.CreateDate,
                CreatedByUserName = i.CreatedBy.UserName,
                LikedStatus = request.UserId.HasValue && i.PostCommentLikeds.Any(j => j.CreatedById == request.UserId) ? i.PostCommentLikeds.FirstOrDefault(j => j.CreatedById == request.UserId)!.LikedStatus : LikedStatus.None
            });

            var postComments = await list.GetPaged(request.Page, request.PageSize);


            return postComments;
        }
    }
}
