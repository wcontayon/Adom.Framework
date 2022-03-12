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
                var httpResult = context.Result switch
                {
                    OkObjectResult okObjResult => new HttpResult(okObjResult),
                    OkResult okResult => new HttpResult(okResult),
                    UnauthorizedObjectResult unauthorizedObjectResult => new HttpResult(unauthorizedObjectResult),
                    UnauthorizedResult unauthorized => new HttpResult(unauthorized),
                    BadRequestObjectResult badRequestObjectResult => new HttpResult(badRequestObjectResult),
                    BadRequestResult badRequestResult => new HttpResult(badRequestResult),
                    _ => new HttpResult(StatusCodes.Status200OK)
                };

                context.Result = controller.Ok(httpResult); 
            }
        }
    }
}
