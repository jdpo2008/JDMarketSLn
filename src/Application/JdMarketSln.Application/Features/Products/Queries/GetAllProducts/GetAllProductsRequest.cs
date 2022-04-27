using AutoMapper;
using JdMarketSln.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsRequest : IMapFrom<GetAllProductsQuery>
    {

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetAllProductsQuery, GetAllProductsRequest>();
        }
    }
}
