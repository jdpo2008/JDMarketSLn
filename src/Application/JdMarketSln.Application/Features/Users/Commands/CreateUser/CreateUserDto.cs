using AutoMapper;
using JdMarketSln.Application.Mappings;
using JdMarketSln.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserDto : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<string> Roles { get; set; }
        public DateTime CreatedAt { get; set; }

        public void Mapping(Profile profile)
        {
           profile.CreateMap<User, CreateUserDto>();  
        }
    }
}
