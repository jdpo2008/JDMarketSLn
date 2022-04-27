using AutoMapper;
using JdMarketSln.Application.Mappings;
using JdMarketSln.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryRequest : IMapFrom<CreateCategoryCommand>
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public void Mapping(Profile profile) 
        {
            profile.CreateMap<CreateCategoryCommand, Category>();
        }
    }
}
