using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Adom.Framework.AspNetCore
{
    public class HttpResult
    {
        internal int _statusCode;
        internal IEnumerable<string> _successMessages = Enumerable.Empty<string>();
        internal IEnumerable<string> _errorMessages = Enumerable.Empty<string>();
        internal string _requestQuery = string.Empty;
        // To Do: find a way to get the payload without Rewind or EnableBuffering.
        // internal object? _payload;
        internal object? _value;
        internal string _httpVerb = HttpVerb.Get.ToString();

        // Study the point (do we need to expose header)
        // internal string[] _requestHeader = Enumerable.Empty<string>().ToArray();
        //internal string[] _responseHeader = Enumerable.Empty<string>().ToArray();

        internal HttpRequestInfo _httpRequestInfo;
        internal HttpResponseInfo _httpResponseInfo;

        #region Constructors

        public HttpResult()
        {
            _statusCode = StatusCodes.Status200OK;
        }

        public HttpResult(int statusCode) => _statusCode = statusCode;

        public HttpResult(int statusCode, string message, bool success = true)
        {
            _statusCode = statusCode;
            if (success)
            {
                _successMessages = new[] { message };
            }
            else
            {
                _errorMessages = new[] { message };
            }
        }

        public HttpResult(object? data, int statusCode, string message, bool success = true)
        {
            _statusCode = statusCode;
            SetValue(data);
            if (success)
            {
                _successMessages = new[] { message };
            }
            else
            {
                _errorMessages = new[] { message };
            }
        }

        public HttpResult(ObjectResult result)
        {
            Debug.Assert(result != null);
            _statusCode = result.StatusCode ?? StatusCodes.Status200OK;
            SetValue(result.Value);
        }

        public HttpResult(OkObjectResult result)
        {
            Debug.Assert(result != null);
            _statusCode = result.StatusCode ?? StatusCodes.Status200OK;
            SetValue(result.Value);
        }

        public HttpResult(OkResult result)
        {
            Debug.Assert(result != null);
            _statusCode = result.StatusCode;
        }

        public HttpResult(UnauthorizedObjectResult result)
        {
            Debug.Assert(result != null);

            _statusCode = result.StatusCode ?? StatusCodes.Status200OK;
            SetValue(result.Value);
            _errorMessages = new[] { "Unauthorized" };
        }

        public HttpResult(UnauthorizedResult result)
        {
            Debug.Assert(result != null);

            _statusCode = result.StatusCode;
            _errorMessages = new[] { "Unauthorized" };
        }

        public HttpResult(BadRequestObjectResult result)
        {
            Debug.Assert(result != null);

            _statusCode = result.StatusCode ?? StatusCodes.Status200OK;
            SetValue(result.Value);
            _errorMessages = new[] { "Bad Request" };
        }

        public HttpResult(BadRequestResult result)
        {
            Debug.Assert(result != null);

            _statusCode = result.StatusCode;
            _errorMessages = new[] { "Bad Request" };
        }

        public HttpResult(NotFoundObjectResult result)
        {
            Debug.Assert(result != null);

            _statusCode = result.StatusCode ?? StatusCodes.Status200OK;
            SetValue(result.Value);
            _errorMessages = new[] { "Not Found" };
        }

        public HttpResult(NotFoundResult result)
        {
            Debug.Assert(result != null);

            _statusCode = result.StatusCode;
            _errorMessages = new[] { "Not Found" };
        }

        public HttpResult(Exception exception)
        {
            Debug.Assert(exception != null);
            Debug.Assert(!string.IsNullOrEmpty(exception.Message));

            _statusCode = StatusCodes.Status500InternalServerError;
            _errorMessages = new[] { exception.Message };
        }

        #endregion

        /// <summary>
        /// Gets the HTTP status code.
        /// </summary>
        public int? StatusCode => _statusCode;

        /// <summary>
        /// Indicates if the HttpResult is success
        /// </summary>
        public bool IsSuccess => _statusCode == StatusCodes.Status200OK;

        public virtual object Value => _value!;

        /// <summary>
        /// Returns the messages of the HttpResult
        /// </summary>
        public IEnumerable<string> Messages => IsSuccess ? _successMessages : _errorMessages;

        internal void SetValue(object? value)
        {
            if (value != null)
            {
                _value = value;
            }
        }

        /// <summary>
        /// Returns the Http verb used
        /// </summary>
        public string Method => _httpVerb;

        /// <summary>
        /// Returns the original request url
        /// </summary>
        public string RequestQuery => _requestQuery;

        #region Extensions

        public static HttpResult NotFound(object? data, string message) => new HttpResult(data, StatusCodes.Status404NotFound, message, false);

        public static HttpResult BadRequest(object? data, string message) => new HttpResult(data, StatusCodes.Status400BadRequest, message, false);

        public void SetAdditionalInforation(HttpContext httpContext)
        {
            Debug.Assert(httpContext != null);

            _httpRequestInfo = new HttpRequestInfo(httpContext);
            _httpResponseInfo = new HttpResponseInfo(httpContext);

            _requestQuery = _httpRequestInfo._requestUri;

            _httpVerb = httpContext.Request.Method.ToUpperInvariant() switch
            {
                "GET" => HttpVerb.Get.ToString(),
                "POST" => HttpVerb.Post.ToString(),
                "PUT" => HttpVerb.Put.ToString(),
                "PATCH" => HttpVerb.Patch.ToString(),
                "HEAD" => HttpVerb.Head.ToString(),
                "DELETE" => HttpVerb.Delete.ToString(),
                _ => HttpVerb.Get.ToString()
            };
        }

        #endregion

        #region IResult

        public Task ExecuteAsync(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public enum HttpVerb
    {
        Get,
        Post,
        Put,
        Delete,
        Patch,
        Options,
        Head
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1815:Remplacer Equals et l'opérateur égal à dans les types valeur", Justification = "No need equals comparison")]
    public readonly struct HttpRequestInfo
    {
        internal readonly string _acceptedEnconding;
        internal readonly string _userAgent;
        internal readonly string _requestUri;
        internal readonly string _authorization;
        // To Do: Find a way to read without Rewinding or EnableBuffering (maybe in the next version of aspnetcore)
        // internal readonly object? _payLaod;
        internal readonly string _schema;

        public HttpRequestInfo(FilterContext context)
        {
            Debug.Assert(context != null);
            Debug.Assert(context.HttpContext != null);

            var httpContext = context.HttpContext;
            _acceptedEnconding = httpContext.Request.Headers["Accept-Encoding"]!;
            _userAgent = httpContext.Request.Headers["User-Agent"]!;
            _requestUri = httpContext.Request.Path!;
            _authorization = httpContext.Request.Headers["Authorization"]!;
            _schema = httpContext.Request.Scheme;
        }

        public HttpRequestInfo(HttpContext httpContext)
        {
            Debug.Assert(httpContext != null);

            _acceptedEnconding = httpContext.Request.Headers["Accept-Encoding"]!;
            _userAgent = httpContext.Request.Headers["User-Agent"]!;
            _requestUri = httpContext.Request.QueryString.Value!;
            _authorization = httpContext.Request.Headers["Authorization"]!;
            _schema = httpContext.Request.Scheme;
        }

        public HttpRequestInfo(HttpRequest request)
        {
            Debug.Assert(request != null);

            _acceptedEnconding = request.Headers["Accept-Encoding"]!;
            _userAgent = request.Headers["User-Agent"]!;
            _requestUri = request.QueryString.Value!;
            _authorization = request.Headers["Authorization"]!;
            _schema = request.Scheme;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1815:Remplacer Equals et l'opérateur égal à dans les types valeur", Justification = "No need equals comparison")]
    public readonly struct HttpResponseInfo
    {
        internal readonly long _contentLength;
        internal readonly string _contentType;

        public HttpResponseInfo(FilterContext context)
        {
            Debug.Assert(context != null);
            Debug.Assert(context.HttpContext != null);

            var httpContext = context.HttpContext;
            _contentLength = httpContext.Response.ContentLength ?? 0;
            _contentType = httpContext.Response.ContentType!;
        }

        public HttpResponseInfo(HttpContext httpContext)
        {
            Debug.Assert(httpContext != null);

            _contentLength = httpContext.Response.ContentLength ?? 0;
            _contentType = httpContext.Response.ContentType!;
        }

        public HttpResponseInfo(HttpResponse response)
        {
            Debug.Assert(response != null);

            _contentLength = response.ContentLength ?? 0;
            _contentType = response.ContentType!;
        }
    }
}
