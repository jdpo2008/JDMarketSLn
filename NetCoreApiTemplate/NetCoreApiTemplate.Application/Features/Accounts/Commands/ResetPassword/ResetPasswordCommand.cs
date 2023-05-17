using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using JDMarketSLn.Application.Common.Exceptions;
using JDMarketSLn.Application.Common.Interfaces;
using JDMarketSLn.Application.Common.Models;
using JDMarketSLn.Application.Interfaces;
using JDMarketSLn.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JDMarketSLn.Application.Features.Accounts.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly IEmailService _emailService;
        public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ILoggerService logger, IEmailService emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<Result> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var filter = _mapper.Map<ResetPasswordRequest>(command);
                var account = await _userManager.FindByEmailAsync(filter.Email);

                if (account == null) throw new ApiException($"No Accounts Registered with {filter.Email}.");
                var result = await _userManager.ResetPasswordAsync(account, filter.Token, filter.Password);
                if (result.Succeeded)
                {
                    return Result.Success((int)HttpStatusCode.OK);
                }
                else
                {
                    _logger.LogDebug("Error at password reset");
                    List<string> errors = new List<string>();
                    errors.Add($"Error: {result.Errors.ToString()}");
                    return Result.Failure(errors, (int)HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while reseting the password.");
                throw new ApiException($"Error occured while reseting the password.  {ex.Message.ToString()}");
            }
        }
    }
}
