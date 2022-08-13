using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Common.Models.Page
{
    public class PageViewModel<T> where T : class
    {
        public PageViewModel() : this(new List<T>(), new Page())
        { }

        public PageViewModel(IList<T> results, Page pageInfo)
        {
            Results = results;
            PageInfo = pageInfo;
        }

        public IList<T> Results { get; set; }
        public Page PageInfo { get; set; }

    }
}
