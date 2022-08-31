using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Common.Models.Queries
{
    public class SearchPostViewModel
    {
        public Guid Id { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
    }
}
