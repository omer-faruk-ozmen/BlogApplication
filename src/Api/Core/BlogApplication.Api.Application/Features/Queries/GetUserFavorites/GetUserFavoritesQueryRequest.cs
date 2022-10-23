using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common.Models.Page;
using BlogApplication.Common.Models.Queries;
using MediatR;

namespace BlogApplication.Api.Application.Features.Queries.GetUserFavorites
{
    public class GetUserFavoritesQueryRequest : BasePagedQuery,IRequest<PageViewModel<GetUserFavoritesViewModel>>
    {
        public GetUserFavoritesQueryRequest(int page, int pageSize, Guid? userId, string? userName=null) : base(page, pageSize)
        {
            UserId = userId;
            UserName = userName;
        }

        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
    }
}
