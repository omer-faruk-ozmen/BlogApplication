using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common;
using BlogApplication.Common.Events.Post;
using BlogApplication.Common.Infrastructure;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.Post.DeleteFavorite
{
    public class DeletePostFavoriteCommandHandler : IRequestHandler<DeletePostFavoriteCommandRequest, bool>
    {
        public async Task<bool> Handle(DeletePostFavoriteCommandRequest request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: BlogConstants.FavoriteExchangeName,
                exchangeType: BlogConstants.DefaultExchangeType,
                queueName: BlogConstants.DeletePostFavoriteQueueName,
                obj: new DeletePostFavoriteEvent()
                {
                    CreatedBy = request.UserId,
                    PostId = request.PostId
                });

            return await Task.FromResult(true);
        }
    }
}
