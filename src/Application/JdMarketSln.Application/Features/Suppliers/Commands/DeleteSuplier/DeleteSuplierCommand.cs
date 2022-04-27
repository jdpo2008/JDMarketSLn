using AutoMapper;
using JdMarketSln.Application.Exceptions;
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

namespace JdMarketSln.Application.Features.Suppliers.Commands.DeleteSuplier
{
    public class DeleteSuplierCommand : IRequest<Response<DeleteSuplierDto>>
    {
        [Required]
        public Guid Id { get; set; }

    }

    public class DeleteSuplierCommandHandler : IRequestHandler<DeleteSuplierCommand, Response<DeleteSuplierDto>>
    {
        private readonly ISuplierGenericRepository _SuplierRepository;
        private readonly IMapper _mapper;

        public DeleteSuplierCommandHandler(ISuplierGenericRepository suplierRepository, IMapper mapper)
        {
            _SuplierRepository = suplierRepository;
            _mapper = mapper;
        }
        public async Task<Response<DeleteSuplierDto>> Handle(DeleteSuplierCommand request, CancellationToken cancellationToken)
        {
            var suplier = await _SuplierRepository.GetByIdAsync(request.Id);
            if (suplier == null) throw new ApiException($"Suplier Not Found.");
            await _SuplierRepository.DeleteAsync(suplier);

            var response = _mapper.Map<DeleteSuplierDto>(suplier);

            return new Response<DeleteSuplierDto>(_mapper.Map<DeleteSuplierDto>(response), "Suplier remove successful");
        }
    }

}
