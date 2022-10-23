using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.Post.Delete
{
    public class DeletePostCommandHandler:IRequestHandler<DeletePostCommandRequest,bool>
    {

        private readonly IPostWriteRepository _postWriteRepository;

        public DeletePostCommandHandler(IPostWriteRepository postWriteRepository)
        {
            _postWriteRepository = postWriteRepository;
        }


        public async Task<bool> Handle(DeletePostCommandRequest request, CancellationToken cancellationToken)
        {
            await _postWriteRepository.DeleteAsync(request.Id);
            return true;
        }
    }
}
