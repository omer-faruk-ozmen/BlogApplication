using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogApplication.Common.Models.RequestModels.Post
{
    public class CreatePostCommandRequest : IRequest<Guid>
    {
        public CreatePostCommandRequest(string title, string metaTitle, string summary, string content, bool published, Guid? createdById)
        {
            Title = title;
            MetaTitle = metaTitle;
            Summary = summary;
            Content = content;
            Published = published;
            CreatedById = createdById;
        }

        public CreatePostCommandRequest()
        {

        }


        public string Title { get; set; }
        public string? MetaTitle { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public bool? Published { get; set; }
        public Guid? CreatedById { get; set; }

    }
}
