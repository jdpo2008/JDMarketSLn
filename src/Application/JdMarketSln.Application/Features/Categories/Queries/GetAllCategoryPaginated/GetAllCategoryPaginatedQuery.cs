using AutoMapper;
using JdMarketSln.Application.Interfaces.Repositories;
using JdMarketSln.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Categories.Queries.GetAllCategoryPaginated
{
    public class GetAllCategoryPaginatedQuery : IRequest<PagedResponse<IEnumerable<GetAllCategoryPaginatedDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllCategoryPaginatedHandler : IRequestHandler<GetAllCategoryPaginatedQuery, PagedResponse<IEnumerable<GetAllCategoryPaginatedDto>>>
    {
        private readonly ICategoryGenericRepository _CategoryRepository;
        private readonly IMapper _mapper;

        public GetAllCategoryPaginatedHandler(ICategoryGenericRepository categoryRepository, IMapper mapper)
        {
            _CategoryRepository = categoryRepository;
            _mapper = mapper;   
        }
        public async Task<PagedResponse<IEnumerable<GetAllCategoryPaginatedDto>>> Handle(GetAllCategoryPaginatedQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<GetAllCategoryPaginatedRequest>(request);
            var categories = await _CategoryRepository.GetAllPaginated(filter.PageNumber, filter.PageSize);
            var response = _mapper.Map<IEnumerable<GetAllCategoryPaginatedDto>>(categories);

            return new PagedResponse<IEnumerable<GetAllCategoryPaginatedDto>>(response, filter.PageNumber, filter.PageSize, categories.Count());
        }
    }
}
