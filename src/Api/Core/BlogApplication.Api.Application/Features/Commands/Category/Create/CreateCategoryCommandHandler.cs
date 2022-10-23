using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Category;
using BlogApplication.Api.Application.Interfaces.Repositories.Post;
using BlogApplication.Common.Models.RequestModels.Category;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlogApplication.Api.Application.Features.Commands.Category.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, Guid>
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ICategoryWriteRepository _categoryWriteRepository;

        public CreateCategoryCommandHandler(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
        }

        public async Task<Guid> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {


            Domain.Models.Category category = new()
            {
                Name = request.Name
            };

            var dbCategory = await _categoryReadRepository.GetSingleAsync(p => p.Name == category.Name);

            if (dbCategory is not null)
                throw new Exception("Category name already exists");

            await _categoryWriteRepository.AddAsync(category);

            return category.Id;

        }
    }
}
