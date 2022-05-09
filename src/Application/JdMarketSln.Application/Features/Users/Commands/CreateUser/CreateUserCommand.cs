using AutoMapper;
using JdMarketSln.Application.Enums;
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

namespace JdMarketSln.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Response<CreateUserDto>>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<CreateUserDto>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response<CreateUserDto>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<CreateUserRequest>(command);

            var validatedUser = await _userManager.FindByEmailAsync(request.Email);
            if (validatedUser != null)
            {
                return new Response<CreateUserDto>(null, $"User {request.Email} exist in bd. LogIn in the system", false);
            }

            var defaultUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                
            };

            var result = await _userManager.CreateAsync(defaultUser, request.PasswordHash);

            if(!result.Succeeded)
            {
                var error = "";
                foreach (var item in result.Errors)
                {
                   error += item.Description + " ";
                }
                throw new ApiException($"{error}");
            } 

            await _userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());

            // TODO Send Email Confirmation

            var user = await _userManager.FindByEmailAsync(request.Email);
            var response = _mapper.Map<CreateUserDto>(user);

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();

            return new Response<CreateUserDto>(response);
        }
    }
}
