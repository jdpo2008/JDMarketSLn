using AutoMapper;
using JdMarketSln.Application.Interfaces.Repositories;
using JdMarketSln.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Products.Queries.GetProductsByIdSuplier
{
    public class GetProductsByIdSuplierQuery : IRequest<Response<IEnumerable<GetProductsByIdSuplierDto>>>
    {
        [Required]
        public Guid SuplierId { get; set; }

    }

    public class GetProductsByIdSuplierQueryHandler : IRequestHandler<GetProductsByIdSuplierQuery, Response<IEnumerable<GetProductsByIdSuplierDto>>>
    {

        private readonly IProductGenericRepository _ProductRepository;
        private readonly IMapper _mapper;

        public GetProductsByIdSuplierQueryHandler(IProductGenericRepository productRepository, IMapper mapper)
        {
            _ProductRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetProductsByIdSuplierDto>>> Handle(GetProductsByIdSuplierQuery query, CancellationToken cancellationToken)
        {
            var rq = _mapper.Map<GetProductsByIdSuplierRequest>(query);
            var products = await _ProductRepository.GetProductsByIdSuplier(rq.SuplierId);

            return new Response<IEnumerable<GetProductsByIdSuplierDto>>(_mapper.Map<IEnumerable<GetProductsByIdSuplierDto>>(products));
        }
    }
}
