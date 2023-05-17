using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JDMarketSLn.Application.Features.Accounts.Commands.ConfirmEmail;
using JDMarketSLn.Application.Features.Accounts.Commands.Logout;
using JDMarketSLn.Application.Features.Accounts.Commands.Authenticated;
using JDMarketSLn.Application.Features.Accounts.Commands.ChangePassword;
using JDMarketSLn.Application.Features.Accounts.Commands.ForgotPassword;
using JDMarketSLn.Application.Features.Accounts.Commands.ResetPassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDMarketSLn.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class AccountController : ApiBaseController
    {

        #region --Account Methods--

        [HttpPost("authenticate")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AuthenticatedUser([FromBody] AuthenticatedCommand command)
        {
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            return Ok(await Mediator.Send(new LogoutCommand()));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {

            return Ok(await Mediator.Send(new ForgotPasswordCommand() { Email = request.Email, Origin = Request.Headers["origin"] }));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            var response = await Mediator.Send(new ConfirmEmailCommand() { UserId = userId, Token = code });

            if(!response.Succeeded)
            {
                return base.Content($"<h3>Error al confirmar la cuenta {response.Errors[0]} </h3>", "text/html");
            }

            return base.Content("<h3>Cuenta confirmada correctamente </h3>", "text/html");
        }


        #endregion

    }
}
