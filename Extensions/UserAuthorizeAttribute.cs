using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCApi.Extensions
{
    /// <summary>
    /// 会员登录验证
    /// </summary>
    public class UserAuthorizeAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var loginUser = ServiceLocator.Resolve<IApiTokenService>().GetUserPayloadByToken();
            if (loginUser == null)
            {
                context.Result = new JsonResult(new { status = 0, msg = "Token is expired or not exists." });
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}