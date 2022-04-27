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

namespace JdMarketSln.Application.Features.Suppliers.Queries.GetAllSupliers
{
    public class GetAllSupliersQuery : IRequest<Response<IEnumerable<GetAllSupliersDto>>>
    {
    }

    public class GetAllSupliersQueryHandler : IRequestHandler<GetAllSupliersQuery, Response<IEnumerable<GetAllSupliersDto>>>
    {
        private readonly ISuplierGenericRepository _SuplierRepository;
        private readonly IMapper _mapper;

        public GetAllSupliersQueryHandler(ISuplierGenericRepository suplierRepository, IMapper mapper)
        {
            _SuplierRepository = suplierRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllSupliersDto>>> Handle(GetAllSupliersQuery request, CancellationToken cancellationToken)
        {
            //var validFilter = _mapper.Map<GetAllProductsRequest>(request);
            var products = await _SuplierRepository.GetAllAsync();
            var response = _mapper.Map<IEnumerable<GetAllSupliersDto>>(products);
            return new Response<IEnumerable<GetAllSupliersDto>>(response);

        }

    }
}
