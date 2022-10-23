using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Api.Application.Interfaces.Repositories.Category;
using BlogApplication.Common.Models.RequestModels.Category;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.Category.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest,bool>
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ICategoryWriteRepository _categoryWriteRepository;

        public DeleteCategoryCommandHandler(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Models.Category dbCategory = await _categoryReadRepository.GetByIdAsync(request.Id);

            if (dbCategory is null)
            {
                throw new Exception("Category not found");
            }

            await _categoryWriteRepository.DeleteAsync(dbCategory.Id);


            return true;
        }
    }
}
