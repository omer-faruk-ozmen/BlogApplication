using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Common.Models.Queries
{
    public class GetCategoriesViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
