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

namespace JdMarketSln.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<Response<IEnumerable<GetAllProductsDto>>>
    {

    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Response<IEnumerable<GetAllProductsDto>>>
    {
        private readonly IProductGenericRepository _ProductRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductGenericRepository productRepository, IMapper mapper)
        {
            _ProductRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllProductsDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            //var validFilter = _mapper.Map<GetAllProductsRequest>(request);
            var products = await _ProductRepository.GetAllAsync();
            var response = _mapper.Map<IEnumerable<GetAllProductsDto>>(products);
            return new Response<IEnumerable<GetAllProductsDto>>(response);

        }

    }
}