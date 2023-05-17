using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using JDMarketSLn.Application.Features.Roles.Queries.GetAllRoles;
using JDMarketSLn.Application.Features.Roles.Queries.GetRoleById;
using JDMarketSLn.Application.Common.Security;
using JDMarketSLn.Application.Features.Roles.Commands.CreateRole;
using JDMarketSLn.Application.Features.Users.Commands.DeleteUser;
using JDMarketSLn.Application.Features.Roles.Commands.DeleteRole;
using JDMarketSLn.Application.Features.Roles.Commands.UpdateRole;

namespace JDMarketSLn.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class RoleController : ApiBaseController
    {

        #region -- Roles Methods No Transaccionals --

        [HttpGet()]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get([FromQuery] GetAllRolesQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetRoleByIdQuery() { Id = id }));
        }

        #endregion

        #region -- Roles Methods Transaccionals --

        [HttpPost()]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] CreateRoleCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateRoleRequest request)
        {                                              
            var response = await Mediator.Send(new UpdateRoleCommand { Id = id, Name = request.Name });
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var response = await Mediator.Send(new DeleteRoleCommand { Id = id });
            return Ok(response);
        }

        #endregion
    }
}
