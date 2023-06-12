using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using ETicaretAPI.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ETicaretAPI.API.Extensions
{
    public static class ConfigureExceptionHandlerExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder application, Serilog.ILogger logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature == null)
                        return;

                    context.Response.StatusCode = exceptionHandlerFeature.Error.GetType().IsAssignableTo(typeof(BaseException)) ?
                    (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.InternalServerError;

                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    logger.Error(exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);

                    var exceptionTypeName = exceptionHandlerFeature.Error.GetType().Name;
                    exceptionTypeName = exceptionTypeName.Substring(0, exceptionTypeName.Length - 9);

                    //Kullanıcıya sonuç döneceğiz
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        ErrorType = exceptionTypeName,
                        Message = exceptionHandlerFeature.Error.Message,
                        Succeeded = false
                    }));
                });
            });
        }
    }
}
