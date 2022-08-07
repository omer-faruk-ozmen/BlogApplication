using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Common;
using BlogApplication.Common.Events.Post;
using BlogApplication.Common.Events.PostComment;
using BlogApplication.Common.Infrastructure;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.PostComment.DeleteLikes
{
    public class DeletePostCommentLikesCommandHandler : IRequestHandler<DeletePostCommentLikesCommandRequest, bool>
    {
        public async Task<bool> Handle(DeletePostCommentLikesCommandRequest request, CancellationToken cancellationToken)
        {

            QueueFactory.SendMessageToExchange(exchangeName: BlogConstants.LikesExchangeName,
                exchangeType: BlogConstants.DefaultExchangeType,
                queueName: BlogConstants.DeletePostCommentLikesQueueName,
                obj: new DeletePostCommentLikesEvent()
                {
                    PostCommentId = request.PostCommentId,
                    CreatedBy = request.UserId,
                });
            return await Task.FromResult(true);

        }
    }
}
