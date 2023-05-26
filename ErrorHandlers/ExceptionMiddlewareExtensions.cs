using FootballScoresApi.Helpers;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Net.Mime;

namespace FootballScoresApi.ErrorHandlers
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    if (contextFeature != null)
                    {
                        Log.Logger.Error($"{ResponseMessage.GLOBAL_INTERNAL_SERVER_ERROR} {contextFeature.Error}");
                        await context.Response.WriteAsync(new Error
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Path = context.Request.Path
                        }.ToString());
                    }
                });
            });
        }
    }
}
