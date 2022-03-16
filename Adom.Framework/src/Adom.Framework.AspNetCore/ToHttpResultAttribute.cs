using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Adom.Framework.AspNetCore
{
    /// <summary>
    /// Attribute used to mapp IActionResult to HttpResult
    /// </summary>
    public sealed class ToHttpResultAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (!(context?.Controller is ControllerBase controller)) return;

            if (context != null)
            {
                HttpResult httpResult;
                if (context.Result == null)
                {
                    if (context.Exception != null)
                    {
                        httpResult = new HttpResult(context.Exception);
                    }
                    else
                    {
                        httpResult = new HttpResult(StatusCodes.Status500InternalServerError, "Exception occured");
                    }
                }
                else
                {
                    httpResult = context.Result switch
                    {
                        OkObjectResult okObjResult => new HttpResult(okObjResult),
                        OkResult okResult => new HttpResult(okResult),
                        UnauthorizedObjectResult unauthorizedObjectResult => new HttpResult(unauthorizedObjectResult),
                        UnauthorizedResult unauthorized => new HttpResult(unauthorized),
                        BadRequestObjectResult badRequestObjectResult => new HttpResult(badRequestObjectResult),
                        BadRequestResult badRequestResult => new HttpResult(badRequestResult),
                        _ => new HttpResult(context.Result as ObjectResult)
                    };
                }               

                httpResult.SetAdditionalInforation(context.HttpContext);

                context.Result = controller.Ok(httpResult); 
            }
        }
    }
}
