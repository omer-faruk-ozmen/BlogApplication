using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogApplication.Common.Models.RequestModels.Category
{
    public class CreateCategoryCommandRequest : IRequest<Guid>
    {
        public CreateCategoryCommandRequest(string name)
        {
            Name = name;
        }
        public CreateCategoryCommandRequest()
        {
            
        }
        public string Name { get; set; }
    }
}
