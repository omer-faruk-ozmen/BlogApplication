using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common;
using BlogApplication.Common.Events.Post;
using BlogApplication.Common.Infrastructure;
using BlogApplication.Common.Models.RequestModels.Post;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.Post.CreateLikes
{
    public class CreatePostLikesCommandHandler : IRequestHandler<CreatePostLikesCommandRequest, bool>
    {
        public async Task<bool> Handle(CreatePostLikesCommandRequest request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: BlogConstants.LikesExchangeName,
                exchangeType: BlogConstants.DefaultExchangeType,
                queueName: BlogConstants.CreatePostLikesQueueName,
                obj: new CreatePostLikesEvent()
                {
                    PostId = request.PostId,
                    CreatedBy = request.CreatedBy,
                    LikedStatus = request.LikedStatus
                });
            return await Task.FromResult(true);
        }
    }
}
