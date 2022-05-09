using AutoMapper;
using JdMarketSln.Application.Exceptions;
using JdMarketSln.Application.Wrappers;
using JdMarketSln.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHndler : IRequestHandler<DeleteUserCommand, Response<string>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public DeleteUserCommandHndler(UserManager<User> userManager, IMapper Mapper)
        {
            _userManager = userManager;
            _mapper = Mapper;   
        }

        public async Task<Response<string>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var request =  _mapper.Map<DeleteUserRequest>(command);

            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            if (user == null)
            {
                throw new ApiException("User not found");
            }

            var result = await _userManager.DeleteAsync(user);
            string message = "";
            if(!result.Succeeded)
            {
                message = result.Errors.ToString();
            }

            message = $"User: {user.Email} delete succefull";

            return new Response<string>(message);
        }
    }
}
