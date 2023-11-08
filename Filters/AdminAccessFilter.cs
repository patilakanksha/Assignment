using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Assignment.Filters
{
    public class AdminAccessFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            if (user != null && user.IsInRole("admin"))
            {
                // User has the "Admin" role, so allow access to the controller.
                base.OnActionExecuting(filterContext);
            }
            else
            {
                // User does not have the "Admin" role, so redirect to an unauthorized page.
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error404", // Create an "Unauthorized" view for non-admin users.
                };

            }
        }
    }
}