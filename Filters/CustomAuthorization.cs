using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Assignment.Filters
{
    public class CustomAuthorization : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            Controller controller = filterContext.Controller as Controller;

            if (controller != null)
            {
                if (session != null && session["Username"] == null)
                {
                    var routeValues = new RouteValueDictionary(new
                    {
                        controller = "Account",
                        action = "Login"
                    });
                    filterContext.Result = new RedirectToRouteResult(routeValues);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}