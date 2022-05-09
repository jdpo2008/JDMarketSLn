using AutoMapper;
using JdMarketSln.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Users.Commands.UserAuthenticated
{
    public class UserAuthenticatedRequest : IMapFrom<UserAuthenticatedCommand>
    {
        public string Email { get; set; } = default;
        public string Password { get; set; } = default;

        public void Mapping(Profile profile) 
        {
           profile.CreateMap<UserAuthenticatedCommand, UserAuthenticatedRequest>();   
        }
    }
}
