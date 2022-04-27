using AutoMapper;
using JdMarketSln.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Suppliers.Queries.GetSuplierById
{
    public class GetSuplierByIdRequest : IMapFrom<GetSuplierByIdQuery>
    {
        public Guid Id { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetSuplierByIdQuery, GetSuplierByIdRequest>();
        }
    }
}
