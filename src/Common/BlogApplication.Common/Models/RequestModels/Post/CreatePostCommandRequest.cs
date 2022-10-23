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
  
        public string Title { get; set; }
        public string? MetaTitle { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public bool? Published { get; set; }
        public Guid[] CategoryIds { get; set; }
        public Guid[] TagIds { get; set; }

    }
}
