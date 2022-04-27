using AutoMapper;
using JdMarketSln.Application.Mappings;
using JdMarketSln.Application.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Categories.Queries.GetAllCategoryPaginated
{
    public class GetAllCategoryPaginatedRequest: ParameterRequest, IMapFrom<GetAllCategoryPaginatedQuery>
    {

        public void Mapping(Profile profile)
        {
           profile.CreateMap<GetAllCategoryPaginatedQuery, GetAllCategoryPaginatedRequest>();   
        }
    }
}
