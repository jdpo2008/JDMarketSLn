using AutoMapper;
using JDMarketSLn.Application.Common.Mappings;
using JDMarketSLn.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Application.Features.Roles.Queries.GetRoleById
{
    public class GetRoleByIdDto : IMapFrom<ApplicationRole>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationRole, GetRoleByIdDto>();
        }
    }
}
