using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common;
using BlogApplication.Common.Events.PostComment;
using BlogApplication.Common.Infrastructure;
using BlogApplication.Common.Models.RequestModels.PostComment;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.PostComment.CreateLikes
{
    public class CreatePostCommentLikesCommandHandler : IRequestHandler<CreatePostCommentLikesCommandRequest, bool>
    {
        public async Task<bool> Handle(CreatePostCommentLikesCommandRequest request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: BlogConstants.LikesExchangeName,
                exchangeType: BlogConstants.DefaultExchangeType,
                queueName: BlogConstants.CreatePostCommentLikesQueueName,
                obj: new CreatePostCommentLikesEvent()
                {
                    CreatedBy = request.CreatedBy,
                    LikedStatus = request.LikedStatus,
                    PostCommentId = request.PostCommentId
                });

            return await Task.FromResult(true);
        }
    }
}
