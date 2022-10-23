using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Domain.Models;

namespace BlogApplication.Api.Application.Interfaces.Services
{
    public interface IPostService
    {
        public Task AddToPhysicalFilePath(Post dbPost);
    }
}
