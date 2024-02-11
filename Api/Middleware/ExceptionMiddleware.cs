/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */

using System.Net;
using Api.Errors;

namespace Api.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        } catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var response = new ErrorsDto(new List<ErrorDto>{
                new() {
                    Name ="An error has occurred",
                    Message = ex.Message
                }
            });

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
