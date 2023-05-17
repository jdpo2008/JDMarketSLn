using MediatR;
using Microsoft.AspNetCore.Identity;
using JDMarketSLn.Application.Common.Models;
using JDMarketSLn.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JDMarketSLn.Application.Features.Accounts.Commands.Logout
{
    public class LogoutCommand : IRequest<Result>
    {
    }

    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public LogoutCommandHandler(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();

            return Result.Success((int)HttpStatusCode.OK);
        }
    }

}