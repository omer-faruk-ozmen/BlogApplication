using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common;
using BlogApplication.Common.Events.Post;
using BlogApplication.Common.Infrastructure;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.Post.DeleteLikes
{
    public class DeletePostLikesCommandHandler : IRequestHandler<DeletePostLikesCommandRequest, bool>
    {
        
        public async Task<bool> Handle(DeletePostLikesCommandRequest request, CancellationToken cancellationToken)
        {

            QueueFactory.SendMessageToExchange(exchangeName: BlogConstants.LikesExchangeName,
                exchangeType: BlogConstants.DefaultExchangeType,
                queueName: BlogConstants.DeletePostLikesQueueName,
                obj: new DeletePostLikesEvent()
                {
                    PostId = request.PostId,
                    CreatedBy = request.UserId
                });

            return await Task.FromResult(true);
        }
    }
}
