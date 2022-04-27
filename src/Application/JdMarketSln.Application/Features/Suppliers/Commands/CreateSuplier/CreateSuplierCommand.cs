using AutoMapper;
using JdMarketSln.Application.Interfaces.Repositories;
using JdMarketSln.Application.Wrappers;
using JdMarketSln.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Suppliers.Commands.CreateSuplier
{
    public class CreateSuplierCommand : IRequest<Response<CreateSuplierDto>>
    {
        [Required]
        public string BusinessName { get; set; }
        [Required]
        public string TaxIdentifier { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Adrress { get; set; }


    }

    public class CreateSuplierCommandHandler : IRequestHandler<CreateSuplierCommand, Response<CreateSuplierDto>>
    {
        private readonly ISuplierGenericRepository _SuplierRepository;
        private readonly IMapper _mapper;

        public CreateSuplierCommandHandler(ISuplierGenericRepository suplierRepository, IMapper mapper)
        {
            _SuplierRepository = suplierRepository;
            _mapper = mapper;
        }
        public async Task<Response<CreateSuplierDto>> Handle(CreateSuplierCommand request, CancellationToken cancellationToken)
        {
            Suplier response = await _SuplierRepository.CreateAsync(_mapper.Map<Suplier>(request));

            return new Response<CreateSuplierDto>(_mapper.Map<CreateSuplierDto>(response), "Suplier created successfull");
        }
    }


}
