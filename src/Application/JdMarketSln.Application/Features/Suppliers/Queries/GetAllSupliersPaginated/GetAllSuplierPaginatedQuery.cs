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

namespace JdMarketSln.Application.Features.Suppliers.Queries.GetAllSupliersPaginated
{
    public class GetAllSuplierPaginatedQuery : IRequest<PagedResponse<IEnumerable<GetAllSupliersPaginatedDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllProductsPaginatedQueryHandler : IRequestHandler<GetAllSuplierPaginatedQuery, PagedResponse<IEnumerable<GetAllSupliersPaginatedDto>>>
    {
        private readonly ISuplierGenericRepository _SuplierRepository;
        private readonly IMapper _mapper;

        public GetAllProductsPaginatedQueryHandler(ISuplierGenericRepository suplierRepository, IMapper mapper)
        {
            _SuplierRepository = suplierRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllSupliersPaginatedDto>>> Handle(GetAllSuplierPaginatedQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllSuplierPaginatedRequest>(request);
            var products = await _SuplierRepository.GetAllPaginated(validFilter.PageNumber, validFilter.PageSize);
            var response = _mapper.Map<IEnumerable<GetAllSupliersPaginatedDto>>(products);

            return new PagedResponse<IEnumerable<GetAllSupliersPaginatedDto>>(response, validFilter.PageNumber, validFilter.PageSize, products.Count());
        }
    }
}
