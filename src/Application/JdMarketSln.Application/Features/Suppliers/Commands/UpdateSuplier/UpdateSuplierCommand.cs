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

namespace JdMarketSln.Application.Features.Suppliers.Commands.UpdateSuplier
{
    public class UpdateSuplierCommand : IRequest<Response<UpdateSuplierDto>>
    {
        public Guid Id { get; set; }
        public string BusinessName { get; set; }
        public string TaxIdentifier { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Adrress { get; set; }
    }

    public class UpdateSuplierCommandHandler: IRequestHandler<UpdateSuplierCommand, Response<UpdateSuplierDto>>
    {

        private readonly ISuplierGenericRepository _SuplierRepository;
        private readonly IMapper _mapper;

        public UpdateSuplierCommandHandler(ISuplierGenericRepository suplierRepository, IMapper mapper)
        {
            _SuplierRepository = suplierRepository;
            _mapper = mapper;
        }

        public async Task<Response<UpdateSuplierDto>> Handle(UpdateSuplierCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateSuplierRequest>(request);
            var suplier = await _SuplierRepository.GetByIdAsync(command.Id);

            if (suplier == null)
            {
                throw new ApiException($"Suplier Not Found.");
            }
            else
            {
                
                suplier.BusinessName = command.BusinessName;
                suplier.TaxIdentifier = command.TaxIdentifier;
                suplier.ContactName = String.IsNullOrEmpty(command.ContactName) ? suplier.ContactName : command.ContactName;
                suplier.ContactPhone = String.IsNullOrEmpty(command.ContactPhone) ? suplier.ContactPhone : command.ContactPhone;
                suplier.ContactEmail = String.IsNullOrEmpty(command.ContactEmail) ? suplier.ContactEmail : command.ContactEmail;
                suplier.Country = String.IsNullOrEmpty(command.Country) ? suplier.Country : command.Country;
                suplier.City = String.IsNullOrEmpty(command.City) ? suplier.City : command.City;
                suplier.Adrress = String.IsNullOrEmpty(command.Adrress) ? suplier.Adrress : command.Adrress;

                await _SuplierRepository.UpdateAsync(suplier);

                return new Response<UpdateSuplierDto>(_mapper.Map<UpdateSuplierDto>(suplier), "Suplier Updated Successufull");
            }
        }
    }
}
