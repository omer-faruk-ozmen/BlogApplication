using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogApplication.Common.Models.RequestModels.Tag
{
    public class CreateTagCommandRequest : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}
