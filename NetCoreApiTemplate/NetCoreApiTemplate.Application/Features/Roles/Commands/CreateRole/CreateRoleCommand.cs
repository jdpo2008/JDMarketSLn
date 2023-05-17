using MediatR;
using Microsoft.AspNetCore.Identity;
using NetCoreApiTemplate.Application.Common.Models;
using NetCoreApiTemplate.Application.Common.Security;
using NetCoreApiTemplate.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Features.Roles.Commands.CreateRole
{
    [Authorize(Roles = "SuperAdmin")]
    public class CreateRoleCommand : IRequest<Result>
    {
        [Required]
        public string Name { get; set; }
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;   
        }
        public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.Name);

            if(role != null)
            {
                List<string> errors = new List<string>
                { 
                    "Role exist in database"
                };
                return Result.Failure(errors, (int)HttpStatusCode.BadRequest);
            }

            await _roleManager.CreateAsync(new ApplicationRole(request.Name));

            return Result.Success((int)HttpStatusCode.Created);
           
        }
    }


}
