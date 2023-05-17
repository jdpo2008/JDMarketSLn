using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using JDMarketSLn.Application.Common.Security;
using JDMarketSLn.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JDMarketSLn.Application.Features.Users.Queries.GetUserById
{
    [Authorize(Roles = "SuperAdmin")]
    public class GetUserByIdQuery : IRequest<GetUserByIdDto>
    {
        public Guid Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<GetUserByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            var response = _mapper.Map<GetUserByIdDto>(user);

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();

            return response;
        }
    }
}
