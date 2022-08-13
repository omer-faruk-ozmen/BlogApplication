using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Common.Models.Page
{
    public class BasePagedQuery
    {
        public BasePagedQuery(int page, int pageSize)
        {
            PageSize = pageSize;
            Page = page;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
