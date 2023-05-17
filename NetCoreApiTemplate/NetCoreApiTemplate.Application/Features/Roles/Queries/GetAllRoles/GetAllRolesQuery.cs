using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using JDMarketSLn.Application.Common.Models;
using JDMarketSLn.Application.Common.Security;
using JDMarketSLn.Application.Features.Users.Queries.GetAllUsers;
using JDMarketSLn.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JDMarketSLn.Application.Features.Roles.Queries.GetAllRoles
{
    [Authorize(Roles = "SuperAdmin")]
    public class GetAllRolesQuery : IRequest<PaginatedList<GetAllRolesDto>>
    {
        public int nPageNumber { get; set; }
        public int nPageSize { get; set; }
    }

    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, PaginatedList<GetAllRolesDto>>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        public GetAllRolesQueryHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
           _roleManager = roleManager;   
           _mapper = mapper;    
        }
        public async Task<PaginatedList<GetAllRolesDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleManager.Roles.ProjectTo<GetAllRolesDto>(_mapper.ConfigurationProvider)
              .PaginatedListAsync(request.nPageNumber, request.nPageSize);
        }
    }
}