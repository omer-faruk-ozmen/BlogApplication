
using System.Security.Claims;
using AutoMapper;
using BlogApplication.Api.Application.Interfaces.Repositories.Category;
using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using BlogApplication.Api.Application.Interfaces.Repositories.Tag;
using BlogApplication.Api.Application.Interfaces.Repositories.User;
using BlogApplication.Api.Application.Interfaces.Services;
using BlogApplication.Api.Domain.Models;
using BlogApplication.Common.Models.RequestModels.Post;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlogApplication.Api.Application.Features.Commands.Post.Create
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommandRequest, Guid>
    {
        private readonly IPostWriteRepository _postWriteRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostService _postService;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ITagReadRepository _tagReadRepository;
        private readonly IUserReadRepository _userReadRepository;


        public CreatePostCommandHandler(IPostWriteRepository postWriteRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IPostService postService, ICategoryReadRepository categoryReadRepository, ITagReadRepository tagReadRepository, IUserReadRepository userReadRepository)
        {
            _postWriteRepository = postWriteRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _postService = postService;
            _categoryReadRepository = categoryReadRepository;
            _tagReadRepository = tagReadRepository;
            _userReadRepository = userReadRepository;
        }

        public async Task<Guid> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
        {

            Guid? userId = Guid.Parse(_httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);



            List<Domain.Models.Category> categories = await _categoryReadRepository.GetList(p => request.CategoryIds.Contains(p.Id));

            List<Domain.Models.Tag> tags = await _tagReadRepository.GetList(p => request.TagIds.Contains(p.Id));


            Domain.Models.Post tPost = new Domain.Models.Post
            {
                Id = Guid.NewGuid(),
                Content = request.Content,
                Title = request.Title,
                Published = (bool)request.Published,
                Summary = request.Summary,
                MetaTitle = request.Summary,
                CreatedById = (Guid)userId
            };



            foreach (var tag in tags)
            {
                tPost.PostTags.Add(new PostTag()
                {
                    TagId = tag.Id
                });
            }

            foreach (var category in categories)
            {
                tPost.PostCategories.Add(new PostCategory()
                {
                    CategoryId = category.Id
                });
            }

            await _postService.AddToPhysicalFilePath(tPost);

            await _postWriteRepository.AddAsync(tPost);

            return tPost.Id;
        }
    }
}
