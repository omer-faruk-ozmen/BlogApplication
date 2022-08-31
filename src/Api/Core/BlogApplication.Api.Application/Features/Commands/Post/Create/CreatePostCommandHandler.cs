using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using BlogApplication.Api.Application.Interfaces.Repositories.PostComment;
using BlogApplication.Common.Models.RequestModels.Post;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.Post.Create
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommandRequest, Guid>
    {
        private readonly IPostWriteRepository _postWriteRepository;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IPostWriteRepository postWriteRepository, IMapper mapper)
        {
            _postWriteRepository = postWriteRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
        {
            var dbPost = _mapper.Map<Domain.Models.Post>(request);

            dbPost.MetaTitle = dbPost.Summary;
            dbPost.CreatedById = Guid.Parse("13d7d772-38ea-4ff2-92f0-64d6f3bf6b92");

            await _postWriteRepository.AddAsync(dbPost);

            return dbPost.Id;
        }
    }
}
