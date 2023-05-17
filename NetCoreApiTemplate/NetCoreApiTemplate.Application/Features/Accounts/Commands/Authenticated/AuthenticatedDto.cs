using AutoMapper;
using JDMarketSLn.Application.Common.Mappings;
using JDMarketSLn.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Application.Features.Accounts.Commands.Authenticated
{
    public class AuthenticatedDto : IMapFrom<ApplicationUser>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public bool TwoFactorEnabled { get; set; }

        public TokenResponse Token { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, AuthenticatedDto>();
        }
    }

    public class TokenResponse
    {
        public string AccessToken { get; set; }
        //public string RefresToken { get; set; }
        public DateTime Expires { get; set; }
    }
}
