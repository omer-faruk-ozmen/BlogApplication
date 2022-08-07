using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogApplication.Common.Models.RequestModels.PostComment
{
    public class CreatePostCommentCommandRequest : IRequest<Guid>
    {
        public CreatePostCommentCommandRequest(Guid postId, string title, string content, Guid createdById)
        {
            PostId = postId;
            Title = title;
            Content = content;
            CreatedById = createdById;
        }

        public CreatePostCommentCommandRequest()
        {

        }

        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid CreatedById { get; set; }
    }
}
