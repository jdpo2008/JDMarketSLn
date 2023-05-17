using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetCoreApiTemplate.Application.Common.Security;
using NetCoreApiTemplate.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Features.Roles.Queries.GetRoleById
{
    [Authorize(Roles = "SuperAdmin")]
    public class GetRoleByIdQuery : IRequest<GetRoleByIdDto>
    {
        public Guid Id { get; set; }
    }

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, GetRoleByIdDto>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        public GetRoleByIdQueryHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<GetRoleByIdDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleManager.FindByIdAsync(request.Id.ToString());
            var response = _mapper.Map<GetRoleByIdDto>(roles);

            return response;
        }
    }
}
