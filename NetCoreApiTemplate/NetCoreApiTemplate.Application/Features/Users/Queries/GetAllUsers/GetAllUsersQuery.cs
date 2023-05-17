using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreApiTemplate.Application.Common.Models;
using NetCoreApiTemplate.Application.Common.Security;
using NetCoreApiTemplate.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Features.Users.Queries.GetAllUsers
{
    [Authorize(Roles = "SuperAdmin")]
    public class GetAllUsersQuery : IRequest<PaginatedList<GetAllUsersDto>>
    {
        public int nPageNumber { get; set; }
        public int nPageSize { get; set; }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<GetAllUsersDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GetAllUsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.Users.ProjectTo<GetAllUsersDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.nPageNumber, request.nPageSize);
        }
    }
}
