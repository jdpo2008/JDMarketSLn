using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreApiTemplate.Application.Features.Users.Commands.CreateUser;
using NetCoreApiTemplate.Application.Features.Users.Commands.DeleteUser;
using NetCoreApiTemplate.Application.Features.Users.Queries.GetAllUsers;
using NetCoreApiTemplate.Application.Features.Users.Queries.GetUserById;
using System;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class UserController : ApiBaseController
    {

        #region --Users Methods No Transaccionales--

        [HttpGet()]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllUser([FromQuery] GetAllUsersQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery() { Id = id }));
        }

        #endregion

        #region --Users Methods Transaccionales--

        [HttpPost()]
        //[Authorize(Policy = "AdminPolicy")]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {

            command.Origin = Request.Headers["origin"];

            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(CreateUser), response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var response = await Mediator.Send(new DeleteUserCommand { Id = id });
            return Ok(response);
        }

        #endregion
    }
}
