using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogApplication.Common.Models.RequestModels.Category
{
    public class DeleteCategoryCommandRequest : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
