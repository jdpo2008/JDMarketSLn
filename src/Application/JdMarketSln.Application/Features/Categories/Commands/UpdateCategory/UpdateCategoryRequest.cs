using AutoMapper;
using JdMarketSln.Application.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryRequest : IMapFrom<UpdateCategoryCommand>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string CategoryDescription { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCategoryCommand, UpdateCategoryRequest>();
        }
    }
}
