using AutoMapper;
using NetCoreApiTemplate.Application.Common.Mappings;
using NetCoreApiTemplate.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Features.Roles.Queries.GetAllRoles
{
    public class GetAllRolesDto : IMapFrom<ApplicationRole>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationRole, GetAllRolesDto>();
        }
    }
}
