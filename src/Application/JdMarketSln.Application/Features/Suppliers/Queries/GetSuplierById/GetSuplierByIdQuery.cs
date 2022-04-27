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

namespace JdMarketSln.Application.Features.Suppliers.Queries.GetSuplierById
{
    public class GetSuplierByIdQuery : IRequest<Response<GetSuplierByIdDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetSuplierByIdQueryHandler : IRequestHandler<GetSuplierByIdQuery, Response<GetSuplierByIdDto>>
    {
        private readonly ISuplierGenericRepository _SuplierRepository;
        private readonly IMapper _mapper;

        public GetSuplierByIdQueryHandler(ISuplierGenericRepository SuplierRepository, IMapper Mapper)
        {
            _SuplierRepository = SuplierRepository;
            _mapper = Mapper;
        }

        public async Task<Response<GetSuplierByIdDto>> Handle(GetSuplierByIdQuery request, CancellationToken cancellationToken)
        {
            var rq = _mapper.Map<GetSuplierByIdQuery>(request);
            var suplier = await _SuplierRepository.GetByIdAsync(rq.Id);
            var response = _mapper.Map<GetSuplierByIdDto>(suplier);
            return new Response<GetSuplierByIdDto>(response);
        }
    }
}
