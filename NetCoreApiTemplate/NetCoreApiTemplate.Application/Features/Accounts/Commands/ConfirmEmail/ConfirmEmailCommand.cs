using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using NetCoreApiTemplate.Application.Common.Models;
using NetCoreApiTemplate.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Features.Accounts.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<Result>
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Token { get; set; }
    }

    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

            if (user == null)
            {
                List<string> errors = new List<string>
                {
                    "User not found"
                };

                return Result.Failure(errors, 400);
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
            {
                List<string> errors = new List<string>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                return Result.Failure(errors, 400);
            }

            return Result.Success(200);
        }
    }


}
