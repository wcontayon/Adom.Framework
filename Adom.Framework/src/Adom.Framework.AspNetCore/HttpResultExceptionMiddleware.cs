using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Adom.Framework.AspNetCore
{
    /// <summary>
    /// Add a middleware to return a <see cref="HttpResult"/>
    /// when an exception occurs
    /// </summary>
    public class ExceptionToHttpResultMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionToHttpResultMiddleware(RequestDelegate request) => _next = request;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            Debug.Assert(httpContext != null);

            try
            {
                await _next(httpContext).ConfigureAwait(false);
            }
#pragma warning disable CA1031 // Ne pas intercepter les types d'exception générale
            catch (Exception ex)
#pragma warning restore CA1031 // Ne pas intercepter les types d'exception générale
            {
                await HandleExceptionAsync(httpContext!, ex).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // We return a StatusCode 200OK.
            // The Http response contains the underlying http error code and the exception message
            context.Response.StatusCode = StatusCodes.Status200OK;
            var httpResult = new HttpResult(exception);
            httpResult.SetAdditionalInforation(context);
            return context.Response.WriteAsync(JsonConvert.SerializeObject(httpResult));
        }

        private static string GetHeaderString(Microsoft.AspNetCore.Http.HttpContext context)
        {
            var responseHeadersString = new StringBuilder();
            foreach (var header in context.Request.Headers)
            {
                responseHeadersString.Append($"{header.Key}: {string.Join(", ", header.Value)}{Environment.NewLine}");
            }

            return responseHeadersString.ToString();
        }
    }

    public static class ExceptionToHttpResultMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionToHttpResult(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionToHttpResultMiddleware>();
            return app;
        }
    }
}
