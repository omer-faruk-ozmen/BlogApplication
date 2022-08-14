using System.Net;
using System.Net.Mime;
using BlogApplication.Api.WebApi.Results;
using BlogApplication.Common.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace BlogApplication.Api.WebApi.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder app,
            bool includeExceptionDetails = false,
            bool useDefaultHandlingResponse = true,
            Func<HttpContext, Exception, Task> handleException = null)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(context =>
                {
                    var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

                    if (!useDefaultHandlingResponse && handleException == null)
                        throw new ArgumentException(nameof(handleException),
                            $"{nameof(handleException)} cannot be null when {nameof(useDefaultHandlingResponse)} is false");

                    if (!useDefaultHandlingResponse && handleException != null)
                        return handleException(context, exceptionObject!.Error);

                    return DefaultHandleException(context, exceptionObject!.Error, includeExceptionDetails);
                });
            });

            return app;
        }

        private static async Task DefaultHandleException(HttpContext context, Exception exception,
            bool includeExcepitonDetails)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = $"Internal Server Error";

            if (exception is UnauthorizedAccessException)
                statusCode = HttpStatusCode.Unauthorized;
            if (exception is DatabaseValidationException)
            {
                var validationResponse = new ValidationResponseModel(exception.Message);

                await WriteResponse(context, statusCode, validationResponse);
                return;
            }

            var res = new
            {
                HttpStatusCode = (int)statusCode,
                Detail = includeExcepitonDetails ? exception.ToString() : message
            };

            await WriteResponse(context, statusCode, res);
        }

        private static async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, object responseObj)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(responseObj);
        }

    }
}
