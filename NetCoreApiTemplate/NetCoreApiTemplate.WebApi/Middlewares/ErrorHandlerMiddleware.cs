using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using NetCoreApiTemplate.Application.Common.Exceptions;
using NetCoreApiTemplate.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            List<string> errors;

            Result result;

            _logger.LogError(error, error.Message);

            switch (error)
            {
                case SqlException e:

                    errors = new List<string> { "Error en la persistencia de la información." };
                    result = Result.Failure(errors, e.Number);

                    break;
                case ApiException e:
                    // custom application error
                    errors = new List<string> { error?.Message ?? "Internal server error." };
                    result = Result.Failure(errors, (int)HttpStatusCode.BadRequest);

                    break;
                case ValidationException e:
                    // custom validation application error
                    errors = new List<string> { error?.Message ?? "One or more validation failures have occurred." };
                    result = Result.Failure(errors, (int)HttpStatusCode.BadRequest);

                    break;
                case KeyNotFoundException e:
                    // not found error
                    errors = new List<string> { error?.Message ?? "Key not found." };
                    result = Result.Failure(errors, (int)HttpStatusCode.BadRequest);

                    break;
                case NotFoundException e:
                    // not found error
                    errors = new List<string> { error?.Message ?? "Resource not found." };
                    result = Result.Failure(errors, (int)HttpStatusCode.NotFound);

                    break;
                case ForbiddenAccessException e:
                    // forbidden error
                    errors = new List<string> { error?.Message ?? "You don´t have access this page o resource." };
                    result = Result.Failure(errors, (int)HttpStatusCode.Forbidden);

                    break;
                default:
                    // unhandled error
                    errors = new List<string> { error?.Message ?? "Internal server error" };
                    result = Result.Failure(errors, (int)HttpStatusCode.InternalServerError);

                    break;
            }
            //string result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(JsonSerializer.Serialize(result));
        }
    }
    }
}
