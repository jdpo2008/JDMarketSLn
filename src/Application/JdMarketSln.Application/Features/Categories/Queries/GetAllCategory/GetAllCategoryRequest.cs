using AutoMapper;
using JdMarketSln.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Categories.Queries.GetAllCategory
{
    public class GetAllCategoryRequest  : IMapFrom<GetAllCategoryQuery>
    {

        public void Mapping(Profile profile)
        {

            profile.CreateMap<GetAllCategoryQuery, GetAllCategoryRequest>();   

        }
    }
}
