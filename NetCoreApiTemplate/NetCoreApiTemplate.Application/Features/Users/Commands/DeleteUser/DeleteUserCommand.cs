using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetCoreApiTemplate.Application.Common.Exceptions;
using NetCoreApiTemplate.Application.Common.Models;
using NetCoreApiTemplate.Application.Common.Security;
using NetCoreApiTemplate.Domain.Entities.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Features.Users.Commands.DeleteUser
{
    [Authorize(Roles = "SuperAdmin")]
    public class DeleteUserCommand : IRequest<Result>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHndler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public DeleteUserCommandHndler(UserManager<ApplicationUser> userManager, IMapper Mapper)
        {
            _userManager = userManager;
            _mapper = Mapper;
        }

        public async Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(command.Id.ToString());

                if (user == null)
                {
                    throw new ApiException("User not found");
                }

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    List<string> errors = new List<string>()
                    {
                        result.Errors.ToString() ?? "Error al eliminar el usuario"
                    };

                    Result.Failure(errors, (int)HttpStatusCode.BadRequest);
                }

                return Result.Success((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message.ToString());
            }

            
        }
    }
}
