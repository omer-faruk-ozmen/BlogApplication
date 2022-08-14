using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogApplication.Common.Models.Queries
{
    public class SearchPostQueryRequest : IRequest<List<SearchPostViewModel>>
    {
        public SearchPostQueryRequest(string searchText)
        {
            SearchText = searchText;
        }

        public SearchPostQueryRequest()
        {

        }

        public string SearchText { get; set; }
    }
}
