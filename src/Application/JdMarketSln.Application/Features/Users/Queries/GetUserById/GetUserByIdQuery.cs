using AutoMapper;
using JdMarketSln.Application.Wrappers;
using JdMarketSln.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<GetUserByIdDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<Response<GetUserByIdDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<GetUserByIdRequest>(request);
            var user = await _userManager.FindByIdAsync(filter.Id.ToString());
            var response = _mapper.Map<GetUserByIdDto>(user);

            return new Response<GetUserByIdDto>(response);
        }
    }
}
