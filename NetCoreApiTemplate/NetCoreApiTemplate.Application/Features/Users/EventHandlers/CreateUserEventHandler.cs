using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NetCoreApiTemplate.Domain.Entities.Identity;
using NetCoreApiTemplate.Domain.Events.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Features.Users.EventHandlers
{
    public class CreateUserEventHandler : INotificationHandler<CreateUserEvent>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CreateUserEventHandler> _logger;

        public CreateUserEventHandler(ILogger<CreateUserEventHandler> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public Task Handle(CreateUserEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("NetCoreApiTemplate Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
