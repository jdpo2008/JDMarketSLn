using AutoMapper;
using JdMarketSln.Application.Mappings;
using JdMarketSln.Application.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Products.Queries.GetAllProductsPaginated
{
    public class GetAllProductsPaginatedRequest : ParameterRequest, IMapFrom<GetAllProductsPaginatedQuery>
    {

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetAllProductsPaginatedQuery, GetAllProductsPaginatedRequest>();
        }
    }
}
