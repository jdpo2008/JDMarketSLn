using AutoMapper;
using JdMarketSln.Application.Wrappers;
using JdMarketSln.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<Response<IEnumerable<GetAllUsersDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Response<IEnumerable<GetAllUsersDto>>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper; 
        }

        public async Task<Response<IEnumerable<GetAllUsersDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<GetAllUserRequest>(request);  
            var users = await _userManager.Users.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var response =  _mapper.Map<IEnumerable<GetAllUsersDto>>(users);

            return new Response<IEnumerable<GetAllUsersDto>>(response);
        }
    }
}
