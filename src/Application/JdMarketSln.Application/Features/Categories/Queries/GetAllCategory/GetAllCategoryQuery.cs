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

namespace JdMarketSln.Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryQuery : IRequest<Response<IEnumerable<GetAllCategoryDto>>>
    {
    }

    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, Response<IEnumerable<GetAllCategoryDto>>>
    {
        private readonly ICategoryGenericRepository _CategoryRepository;
        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(ICategoryGenericRepository categoryRepository, IMapper mapper)
        {
            _CategoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<GetAllCategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await _CategoryRepository.GetAllAsync();
            var response = _mapper.Map<IEnumerable<GetAllCategoryDto>>(categories);

            return new Response<IEnumerable<GetAllCategoryDto>>(response);
        }
    }
}
