using AutoMapper;
using NetCoreApiTemplate.Application.Common.Mappings;
using NetCoreApiTemplate.Application.Features.Accounts.Commands.ResetPassword;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleRequest : IMapFrom<UpdateRoleCommand>
    {

        [Required]
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateRoleCommand, UpdateRoleRequest>();
        }
    }
}
