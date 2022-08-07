using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Common.Events.Post
{
    public class DeletePostLikesEvent
    {
        public Guid PostId { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
