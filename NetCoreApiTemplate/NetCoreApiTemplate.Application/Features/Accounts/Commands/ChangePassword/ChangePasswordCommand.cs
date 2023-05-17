using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NetCoreApiTemplate.Application.Common.Exceptions;
using NetCoreApiTemplate.Application.Common.Interfaces;
using NetCoreApiTemplate.Application.Common.Models;
using NetCoreApiTemplate.Application.Interfaces;
using NetCoreApiTemplate.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Features.Accounts.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Result>
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly IEmailService _emailService;
        public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ILoggerService logger, IEmailService emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<Result> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var filter = _mapper.Map<ChangePasswordRequest>(command);

                var user = await _userManager.FindByEmailAsync(filter.Email);

                if (user == null) throw new ApiException($"No Accounts Registered with {filter.Email}.");

                var result = await _userManager.ChangePasswordAsync(user, filter.OldPassword, filter.NewPassword);

                if (result.Succeeded)
                {
                    return Result.Success((int)HttpStatusCode.OK);
                }
                else
                {
                    _logger.LogDebug("Error at password change");
                    var lstError = new List<string>();
                    lstError.Add($"Error: {result.Errors.ToString()}");
                    return Result.Failure(lstError, (int)HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at password change {ex.Message}");
                throw new ApiException($"Error: {ex.Message}");
            }
        }

    }
}
