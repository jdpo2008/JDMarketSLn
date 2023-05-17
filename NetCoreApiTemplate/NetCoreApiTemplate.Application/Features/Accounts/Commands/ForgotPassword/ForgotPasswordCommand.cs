using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NetCoreApiTemplate.Application.Common.Exceptions;
using NetCoreApiTemplate.Application.Common.Interfaces;
using NetCoreApiTemplate.Application.Common.Models;
using NetCoreApiTemplate.Application.Interfaces;
using NetCoreApiTemplate.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Features.Accounts.Commands.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<Result>
    {

        public string Email { get; set; } = default;
        public string Origin { get; set; } = string.Empty;
    }

    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly IEmailService _emailService;
        public ForgotPasswordCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ILoggerService logger, IEmailService emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var filter = _mapper.Map<ForgotPasswordRequest>(request);
                var account = await _userManager.FindByEmailAsync(filter.Email);

                // always return ok response to prevent email enumeration
                if (account == null)
                {
                    List<string> errors = new List<string>()
                    {
                        "Not account found"
                    };

                    return Result.Failure(errors, (int)HttpStatusCode.NotFound);
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(account);
                var route = "reset-password";
                var _enpointUri = new Uri(string.Concat($"{request.Origin}/", route));

                var result = await _emailService.SendForgotPasswordEmailAsync(filter.Email, account.FirstName, code, cancellationToken);

                if (!result.Succeeded)
                {
                    List<string> erros = new List<string>()
                    {
                        "Email send error"
                    };

                    return Result.Failure(erros, (int)HttpStatusCode.BadRequest);
                }

                return Result.Success((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error: {ex.Message.ToString()}");
                throw new ApiException("Error: " + ex.Message.ToString());
            }


        }
    }
}
