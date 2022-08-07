using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common;
using BlogApplication.Common.Events.Post;
using BlogApplication.Common.Infrastructure;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.Post.CreateFavorite
{
    public class CreatePostFavoriteCommandHandler : IRequestHandler<CreatePostFavoriteCommandRequest, bool>
    {
        public Task<bool> Handle(CreatePostFavoriteCommandRequest request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: BlogConstants.FavoriteExchangeName,
                exchangeType: BlogConstants.DefaultExchangeType,
                queueName: BlogConstants.CreatePostFavoriteQueueName,
                obj: new CreatePostFavoriteEvent()
                {
                    PostId = request.PostId.Value,
                    CreatedBy = request.UserId.Value
                });

            return Task.FromResult(true);
        }
    }
}
