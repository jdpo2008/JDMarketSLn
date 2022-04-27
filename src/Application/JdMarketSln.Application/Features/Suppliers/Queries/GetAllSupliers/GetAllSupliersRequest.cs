using AutoMapper;
using JdMarketSln.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Suppliers.Queries.GetAllSupliers
{
    public class GetAllSupliersRequest : IMapFrom<GetAllSupliersQuery>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetAllSupliersQuery, GetAllSupliersRequest>();
        }
    }
}
