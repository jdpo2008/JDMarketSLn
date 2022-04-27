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

namespace JdMarketSln.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Response<GetProductByIdDto>>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<GetProductByIdDto>>
    {
        private readonly IProductGenericRepository _ProducRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductGenericRepository ProducRepository, IMapper Mapper)
        {
            _ProducRepository = ProducRepository;
            _mapper = Mapper;
        }
        public async Task<Response<GetProductByIdDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var rq = _mapper.Map<GetProductByIdRequest>(request);
            var product = await _ProducRepository.GetByIdAsync(rq.Id);
            var response = _mapper.Map<GetProductByIdDto>(product);
            return new Response<GetProductByIdDto>(response);
        }
    }
}
