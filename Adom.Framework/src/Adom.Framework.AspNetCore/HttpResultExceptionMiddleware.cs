using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
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
        private readonly ILogger _logger;
        private readonly Action<Exception>? _afterHandlerAction;

        public ExceptionToHttpResultMiddleware(RequestDelegate request, ILoggerFactory loggerFactory, Action<Exception>? handlerAction = null)
        {
            _next = request;
            _logger = loggerFactory.CreateLogger<ExceptionToHttpResultMiddleware>();
            _afterHandlerAction = handlerAction;
        }

        public Task InvokeAsync(HttpContext httpContext)
        {
            Debug.Assert(httpContext != null);
            ExceptionDispatchInfo? edi = default;

            try
            {
                var task = _next(httpContext);
                if (!task.IsCompletedSuccessfully)
                {
                    return Awaited(this, httpContext, task).AsTask();
                }
            }
#pragma warning disable CA1031 // Ne pas intercepter les types d'exception générale
            catch (Exception ex)
#pragma warning restore CA1031 // Ne pas intercepter les types d'exception générale
            {
                edi = ExceptionDispatchInfo.Capture(ex);
            }

            static async ValueTask Awaited(ExceptionToHttpResultMiddleware middleware, HttpContext httpContext, Task task)
            {
                ExceptionDispatchInfo? edi = null;
                try
                {
                    await task.ConfigureAwait(false);
                }
#pragma warning disable CA1031 // Ne pas intercepter les types d'exception générale
                catch (Exception exception)
#pragma warning restore CA1031 // Ne pas intercepter les types d'exception générale
                {
                    // Get the Exception, but don't continue processing in the catch block as its bad for stack usage.
                    edi = ExceptionDispatchInfo.Capture(exception);
                }

                if (edi != null)
                {
                    await middleware.HandleExceptionAsync(httpContext, edi).ConfigureAwait(false);
                }
            }

            return HandleExceptionAsync(httpContext!, edi).AsTask();
        }

        private async ValueTask HandleExceptionAsync(HttpContext context, ExceptionDispatchInfo? exceptionDispatch)
        {
            if (exceptionDispatch != null && exceptionDispatch.SourceException != null)
            {
                if (context.Response.HasStarted)
                {
                    // The response has already started. We rethrow the exception
                    exceptionDispatch!.Throw();
                }

                // We return a StatusCode 200OK.
                // The Http response contains the underlying http error code and the exception message
                context.Response.StatusCode = StatusCodes.Status200OK;
                var httpResult = new HttpResult(exceptionDispatch!.SourceException);
                httpResult.SetAdditionalInforation(context);

                // Execute custom Handler before rewrite the response
                if (_afterHandlerAction != null)
                {
                    _afterHandlerAction(exceptionDispatch!.SourceException);
                }
                
                await context.Response.WriteAsync(JsonConvert.SerializeObject(httpResult)).ConfigureAwait(false);
            }
        }

        private static string GetHeaderString(Microsoft.AspNetCore.Http.HttpContext context)
        {
            var responseHeadersString = new StringBuilder();
            foreach (var header in context.Request.Headers)
            {
                responseHeadersString.Append($"{header.Key}: {string.Join(", ", header.Value.ToString())}{Environment.NewLine}");
            }

            return responseHeadersString.ToString();
        }
    }

    public static class ExceptionToHttpResultMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionToHttpResult(this IApplicationBuilder app, Action<Exception>? handlerExceptionAction = null)
        {
            app.UseMiddleware<ExceptionToHttpResultMiddleware>(handlerExceptionAction);
            return app;
        }
    }
}
