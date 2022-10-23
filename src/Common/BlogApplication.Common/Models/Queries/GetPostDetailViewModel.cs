using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Common.Models.Queries
{
    public class GetPostDetailViewModel : BaseFooterRateFavoriteViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public bool Published { get; set; }
        public string CreateByUserName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public IList<PostTag> PostTags { get; set; }
        public IList<PostCategory> PostCategories { get; set; }
    }
}
