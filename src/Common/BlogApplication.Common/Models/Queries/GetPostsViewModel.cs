using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Common.Models.Queries
{
    public class GetPostsViewModel : BaseFooterRateFavoriteViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public bool Published { get; set; }
        public string CreatedByUserName { get; set; }
        public int CommentCount { get; set; }
        public int LikesCount { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
        public ICollection<PostCategory> PostCategories { get; set; }
    }
}
