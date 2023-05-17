using MediatR;
using Microsoft.AspNetCore.Identity;
using JDMarketSLn.Application.Common.Models;
using JDMarketSLn.Application.Common.Security;
using JDMarketSLn.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Application.Features.Roles.Commands.DeleteRole
{
    [Authorize(Roles = "SuperAdmin")]
    public class DeleteRoleCommand : IRequest<Result>
    {
        [Required]
        public Guid Id { get; set; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        public DeleteRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());

            if (role == null) 
            {
                List<string> errors = new List<string> 
                {
                    "Role not found"
                };

                return Result.Failure(errors, (int)HttpStatusCode.BadRequest);
            
            }

            await _roleManager.DeleteAsync(role);

            return Result.Success((int)HttpStatusCode.Accepted);
        }
    }
}
