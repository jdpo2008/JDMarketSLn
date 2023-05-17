using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using JDMarketSLn.Application.Common.Exceptions;
using JDMarketSLn.Application.Common.Models;
using JDMarketSLn.Application.Common.Security;
using JDMarketSLn.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JDMarketSLn.Application.Features.Roles.Commands.UpdateRole
{
    [Authorize(Roles = "SuperAdmin")]
    public class UpdateRoleCommand : IRequest<Result>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result>
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;

        }

        public async Task<Result> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<UpdateRoleRequest>(command);

            ApplicationRole roleToEdit = await _roleManager.FindByIdAsync(command.Id.ToString());

            if (roleToEdit == null)
            {
                throw new NotFoundException($"Role not found in database");
            }

            if (roleToEdit.Name != request.Name)
                roleToEdit.Name = request.Name;

            IdentityResult result = await _roleManager.UpdateAsync(roleToEdit);

            if (!result.Succeeded)
            {
                List<string> errors = new List<string>();

                foreach (var item in result.Errors)
                {
                    errors.Add(item.Description);
                }

                return Result.Failure(errors, (int)HttpStatusCode.BadRequest);
            }

            return Result.Success((int)HttpStatusCode.Accepted);
        }
    }
}
