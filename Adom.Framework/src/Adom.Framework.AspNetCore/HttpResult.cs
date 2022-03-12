using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Adom.Framework.AspNetCore
{
    public class HttpResult
    {
        internal int _statusCode;
        internal IEnumerable<string> _successMessages = Enumerable.Empty<string>();
        internal IEnumerable<string> _errorMessages = Enumerable.Empty<string>();

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

        public HttpResult(OkObjectResult result)
        {
            Debug.Assert(result != null);
            _statusCode = result.StatusCode ?? StatusCodes.Status200OK;
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


        /// <summary>
        /// Gets the HTTP status code.
        /// </summary>
        public int? StatusCode => _statusCode;

        /// <summary>
        /// Indicates if the HttpResult is success
        /// </summary>
        public bool IsSuccess => _statusCode == StatusCodes.Status200OK;

        /// <summary>
        /// Returns the messages of the HttpResult
        /// </summary>
        public IEnumerable<string> Messages => IsSuccess ? _successMessages : _errorMessages;
    }

    /// <summary>
    /// Defines the result of a http call.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpResult<T> : HttpResult
    {
        internal T? _value;
       

        #region Constructors

        public HttpResult(T? data) : base(StatusCodes.Status200OK)
        {
            _value = data;
        }

        public HttpResult(T? data, int statusCode) : base(statusCode)
        {
            _value = data;
        }

        public HttpResult(T? data, int statusCode, string message, bool success) : base(statusCode, message, success)
        {
            _value = data;
        }

        public HttpResult(OkObjectResult result) : base(result)
        {
            Debug.Assert(result != null);

            _value = (T)result.Value;
        }

        public HttpResult(UnauthorizedObjectResult result) : base(result)
        {
            Debug.Assert(result != null);

            _value = (T)result.Value;
        }

        public HttpResult(BadRequestObjectResult result) : base(result)
        {
            Debug.Assert(result != null);

            _value = (T)result.Value;
        }

        #endregion

        /// <summary>
        /// The object result.
        /// </summary>
        public T? Value => _value!;


        #region Extensions

        public HttpResult<T> NotFound(T? data, string message) => new HttpResult<T>(data, StatusCodes.Status404NotFound, message, false);

        public HttpResult<T> BadRequest(T? data, string message) => new HttpResult<T>(data, StatusCodes.Status400BadRequest, message, false);

        #endregion
    }
}
