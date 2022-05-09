using AutoMapper;
using JdMarketSln.Application.Mappings;
using JdMarketSln.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Users.Commands.UserAuthenticated
{
    public class UserAuthenticatedDto : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<string> Roles { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserAuthenticatedDto>();  
        }
    }
}
