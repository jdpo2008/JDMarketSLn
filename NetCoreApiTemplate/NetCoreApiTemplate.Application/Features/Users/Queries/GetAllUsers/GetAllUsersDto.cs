using AutoMapper;
using JDMarketSLn.Application.Common.Mappings;
using JDMarketSLn.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersDto : IMapFrom<ApplicationUser>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, GetAllUsersDto>();
        }
    }
}
