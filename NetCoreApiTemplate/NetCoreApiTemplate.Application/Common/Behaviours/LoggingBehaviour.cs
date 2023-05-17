using NetCoreApiTemplate.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using NetCoreApiTemplate.Application.Interfaces;

namespace NetCoreApiTemplate.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    private readonly ILoggerService _loggerService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IIdentityService identityService, ILoggerService loggerService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _identityService = identityService;
        _loggerService = loggerService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? string.Empty;
        string? userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            userName = await _identityService.GetUserNameAsync(userId);
        }

        _loggerService.LogInfo("NetCoreApiTemplate Request: {Name} {@UserId} {@UserName} {@Request}", requestName, userId, userName, request);

        _logger.LogInformation("NetCoreApiTemplate Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
    }
}
