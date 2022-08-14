using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Common.Models.Queries
{
    public class GetPostCommentsViewModel : BaseFooterRateViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedByUserName { get; set; }
    }
}
