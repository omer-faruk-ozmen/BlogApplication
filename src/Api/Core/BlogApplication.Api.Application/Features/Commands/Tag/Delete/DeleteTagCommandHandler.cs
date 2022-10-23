using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Tag;
using BlogApplication.Common.Models.RequestModels.Tag;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.Tag.Delete
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommandRequest, bool>
    {
        private readonly ITagWriteRepository _tagWriteRepository;
        private readonly ITagReadRepository _tagReadRepository;

        public DeleteTagCommandHandler(ITagWriteRepository tagWriteRepository, ITagReadRepository tagReadRepository)
        {
            _tagWriteRepository = tagWriteRepository;
            _tagReadRepository = tagReadRepository;
        }

        public async Task<bool> Handle(DeleteTagCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Models.Tag dbTag = await _tagReadRepository.GetByIdAsync(request.Id);

            if (dbTag is null)
                return false;

            await _tagWriteRepository.DeleteAsync(dbTag.Id);

            return true;
        }
    }
}
