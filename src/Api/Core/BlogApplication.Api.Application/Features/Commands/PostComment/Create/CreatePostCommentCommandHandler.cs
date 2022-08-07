using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogApplication.Api.Application.Interfaces.Repositories.PostComment;
using BlogApplication.Common.Models.RequestModels.PostComment;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.PostComment.Create
{
    public class CreatePostCommentCommandHandler : IRequestHandler<CreatePostCommentCommandRequest, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IPostCommentWriteRepository _postCommentWriteRepository;

        public CreatePostCommentCommandHandler(IMapper mapper, IPostCommentWriteRepository postCommentWriteRepository)
        {
            _mapper = mapper;
            _postCommentWriteRepository = postCommentWriteRepository;
        }

        public async Task<Guid> Handle(CreatePostCommentCommandRequest request, CancellationToken cancellationToken)
        {
            var dbEntryComment = _mapper.Map<Domain.Models.PostComment>(request);

            await _postCommentWriteRepository.AddAsync(dbEntryComment);

            return dbEntryComment.Id;
        }
    }
}
