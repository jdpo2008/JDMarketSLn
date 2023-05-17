using AutoMapper;
using NetCoreApiTemplate.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Features.Accounts.Commands.Authenticated
{
    public class AuthenticatedRequest : IMapFrom<AuthenticatedCommand>
    {
        public string Email { get; set; } = default;
        public string Password { get; set; } = default;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AuthenticatedCommand, AuthenticatedRequest>();
        }
    }
}
