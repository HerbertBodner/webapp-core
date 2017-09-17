using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WaCore.Common.ExceptionHandling;
using WaCore.Web.Exceptions;

namespace WaCore.Web.Middleware.WebApiExceptionHandler
{
    /// <summary>
    /// A middleware to handle exceptions in Web Api project and generate HTTP response based on exception type.
    /// By default this middleware will handle all exceptions inherited from <see cref="WebApiException"/>.
    /// </summary>
    public class WacWebApiExceptionHandlerMiddleware
    {
        private const string UnhandledExceptionMessage = "Internal server error";

        private readonly RequestDelegate _next;
        private readonly WacWebApiExceptionHandlerOptions _options;

        public WacWebApiExceptionHandlerMiddleware(RequestDelegate next, WacWebApiExceptionHandlerOptions options = null)
        {
            _next = next;
            _options = options ?? new WacWebApiExceptionHandlerOptions();
        }

        public async Task Invoke(HttpContext httpContext, ILogger<WacWebApiExceptionHandlerMiddleware> logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var handlingConfiguration = _options.HandlingConfiguration ?? WacHandling.Prepare<object, WebApiExceptionDto>();

                if (!handlingConfiguration.ContainsHandler<WebApiException>())
                {
                    handlingConfiguration = handlingConfiguration.On<WebApiException>(e =>
                    {
                        var exceptionDto = new WebApiExceptionDto
                        {
                            HttpStatusCode = e.HttpStatusCode,
                            Message = e.ToString()
                        };

                        return WacHandling.Handled(exceptionDto);
                    });
                }

                var result = handlingConfiguration.Catch(ex, throwIfNotHandled: !_options.CatchAll);

                if (!result.Handled)
                {
                    var exceptionDto = new WebApiExceptionDto
                    {
                        Message = UnhandledExceptionMessage,
                        HttpStatusCode = HttpStatusCode.InternalServerError
                    };

                    SendErrorResposnse(exceptionDto, HttpStatusCode.InternalServerError, httpContext);
                }
                else
                {
                    var exceptionDto = result.Result;
                    SendErrorResposnse(exceptionDto, exceptionDto.HttpStatusCode, httpContext);
                }
            }
        }

        private void SendErrorResposnse(object data, HttpStatusCode httpStatusCode, HttpContext httpContext)
        {
            httpContext.Response.StatusCode = (int)httpStatusCode;
            httpContext.Response.ContentType = "application/json";

            var serializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            using (var streamWriter = new StreamWriter(httpContext.Response.Body, Encoding.UTF8))
            {
                using (var jsonWriter = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(jsonWriter, data);
                }
            }
        }
    }
}
