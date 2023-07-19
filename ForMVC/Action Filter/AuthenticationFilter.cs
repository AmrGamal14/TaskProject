using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ForMVC.Action_Filter
{
    public class AuthenticationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string token = context.HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            { // Unauthorized access,
                context.Result = new RedirectResult("~/Authentication/Index");
            }

        }
    }
}
