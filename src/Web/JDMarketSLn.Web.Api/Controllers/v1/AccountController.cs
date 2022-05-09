using JdMarketSln.Application.Features.Users.Commands.CreateUser;
using JdMarketSln.Application.Features.Users.Commands.DeleteUser;
using JdMarketSln.Application.Features.Users.Commands.UserAuthenticated;
using JdMarketSln.Application.Features.Users.Queries.GetAllUsers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JDMarketSLn.Web.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {
        #region --Users Methods--
       
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAllUserPaginated([FromQuery] GetAllUserRequest filter)
        {
            return Ok(await Mediator.Send(new GetAllUsersQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(CreateUser), response);
        }

        [HttpDelete("{id}")]
        //[Authorize]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var response = await Mediator.Send(new DeleteUserCommand { Id = id });
            return Ok(response);
        }

        #endregion

        #region --Account Methods--

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AuthenticatedUser([FromBody] UserAuthenticatedCommand command)
        {
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        #endregion

        #region --Roles Methods--

        #endregion
    }
}
