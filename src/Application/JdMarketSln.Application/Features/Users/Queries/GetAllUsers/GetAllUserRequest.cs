using AutoMapper;
using JdMarketSln.Application.Mappings;
using JdMarketSln.Application.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUserRequest : ParameterRequest, IMapFrom<GetAllUsersQuery>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetAllUsersQuery, GetAllUserRequest>();  
        }
    }
}
