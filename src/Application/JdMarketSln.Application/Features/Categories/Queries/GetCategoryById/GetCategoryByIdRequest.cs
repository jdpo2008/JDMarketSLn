using AutoMapper;
using JdMarketSln.Application.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdRequest : IMapFrom<GetCategoryByIdQuery>
    {
        public Guid Id { get; set; }

        public void Mapping(Profile profile) {
            profile.CreateMap<GetCategoryByIdQuery, GetCategoryByIdRequest>();  
        }
    }
}
