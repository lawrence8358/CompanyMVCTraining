using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _20170703MVC.Controllers
{
    public class CheckKeyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var key = filterContext.HttpContext.Request.QueryString["key"];

            if (key == "quit")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Products" }, { "Action", "Index" } });
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}