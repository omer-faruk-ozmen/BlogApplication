using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Category;
using BlogApplication.Api.Application.Interfaces.Repositories.Tag;
using BlogApplication.Common.Models.RequestModels.Post;
using BlogApplication.Common.Models.RequestModels.Tag;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.Tag.Create
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommandRequest,Guid>
    {
        private readonly ITagReadRepository _tagReadRepository;
        private readonly ITagWriteRepository _tagWriteRepository;

        public CreateTagCommandHandler(ITagReadRepository tagReadRepository, ITagWriteRepository tagWriteRepository)
        {
            _tagReadRepository = tagReadRepository;
            _tagWriteRepository = tagWriteRepository;
        }

        public async Task<Guid> Handle(CreateTagCommandRequest request, CancellationToken cancellationToken)
        {


            Domain.Models.Tag tag = new()
            {
                Name = request.Name
            };

            var dbCategory = await _tagReadRepository.GetSingleAsync(p => p.Name == tag.Name);

            if (dbCategory is not null)
                throw new Exception("Tag name already exists");

            await _tagWriteRepository.AddAsync(tag);

            return tag.Id;
        }
    }
}
