using BlogApplication.Api.Application.Interfaces.Repositories.Category;
using BlogApplication.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Api.Application.Features.Queries.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQueryRequest, List<GetCategoriesViewModel>>
{
    private readonly ICategoryReadRepository _categoryReadRepository;

    public GetCategoriesQueryHandler(ICategoryReadRepository categoryReadRepository)
    {
        _categoryReadRepository = categoryReadRepository;
    }

    public async Task<List<GetCategoriesViewModel>> Handle(GetCategoriesQueryRequest request, CancellationToken cancellationToken)
    {
        var query = _categoryReadRepository.AsQueryable();

        var list = await query.Select(i => new GetCategoriesViewModel()
        {
            Id = i.Id,
            Name = i.Name,
        }).ToListAsync();

        return list;
    }
}