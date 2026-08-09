using Basket.Api.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Basket.Api.Extensions
{
    public static class ErrorHandlingExtension
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature == null) return;

                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Response.ContentType = "application/json";

                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        EntityNotFoundException => (int)HttpStatusCode.NotFound,
                        ValidationErrorException => (int)HttpStatusCode.BadRequest,
                        BadRequestException => (int)HttpStatusCode.BadRequest,
                        ValidationException => (int)HttpStatusCode.BadRequest,
                        OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable,
                        NoDataFoundException => (int)HttpStatusCode.NotFound,
                        AlreadyExistsException => (int)HttpStatusCode.Conflict,
                        NotAuthorizedException => (int)HttpStatusCode.Unauthorized,
                        UnauthenticatedException => (int)HttpStatusCode.Forbidden,
                        InvalidRefreshTokenException => (int)HttpStatusCode.UnprocessableContent,
                        ConflictException => (int)HttpStatusCode.Conflict,
                        ArgumentNullException => (int)HttpStatusCode.BadRequest,
                        FormatException => (int)HttpStatusCode.BadRequest,
                        _ => (int)HttpStatusCode.InternalServerError
                    };

                    var errorResponse = new
                    {
                        statusCode = context.Response.StatusCode,
                        message = contextFeature.Error.GetBaseException().Message
                    };

                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
                });
            });
        }
    }
}
