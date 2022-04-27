using AutoMapper;
using JdMarketSln.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Suppliers.Commands.UpdateSuplier
{
    public class UpdateSuplierRequest : IMapFrom<UpdateSuplierCommand>
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

        public void Mapping(Profile profile) 
        { 
            profile.CreateMap<UpdateSuplierCommand, UpdateSuplierRequest>();
        
        }
    }
}
