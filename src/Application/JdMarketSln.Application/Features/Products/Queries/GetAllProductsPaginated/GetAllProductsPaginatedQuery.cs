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

namespace JdMarketSln.Application.Features.Products.Queries.GetAllProductsPaginated
{
    public class GetAllProductsPaginatedQuery : IRequest<PagedResponse<IEnumerable<GetAllProductsPaginatedDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }

    public class GetAllProductsPaginatedQueryHandler : IRequestHandler<GetAllProductsPaginatedQuery, PagedResponse<IEnumerable<GetAllProductsPaginatedDto>>>
    {
        private readonly IProductGenericRepository _ProductRepository;
        private readonly IMapper _mapper;

        public GetAllProductsPaginatedQueryHandler(IProductGenericRepository productRepository, IMapper mapper)
        {
            _ProductRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllProductsPaginatedDto>>> Handle(GetAllProductsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllProductsPaginatedRequest>(request);
            var products = await _ProductRepository.GetAllIncludeAsync(validFilter.PageNumber, validFilter.PageSize);
            var response = _mapper.Map<IEnumerable<GetAllProductsPaginatedDto>>(products);

            return new PagedResponse<IEnumerable<GetAllProductsPaginatedDto>>(response, validFilter.PageNumber, validFilter.PageSize, products.Count());
        }
    }

}