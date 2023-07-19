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

        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    string token = filterContext.HttpContext.Request.Headers["Authorization"];
        //    var request = filterContext.HttpContext.Request;
        //    var authHeader = request.Headers["Authorization"]; 
        //    if (string.IsNullOrEmpty(authHeader) )
        //    { // Unauthorized access,
        //      filterContext.Result = new RedirectResult("~/Authentication/Index"); 
        //    }
        //}

    }
}
