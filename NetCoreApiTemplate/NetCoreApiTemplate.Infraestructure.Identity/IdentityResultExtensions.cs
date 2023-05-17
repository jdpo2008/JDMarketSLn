using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NetCoreApiTemplate.Application.Common.Models;

namespace NetCoreApiTemplate.Infraestructure.Identity;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success(StatusCodes.Status200OK)
            : Result.Failure(result.Errors.Select(e => e.Description), StatusCodes.Status400BadRequest);
    }
}
