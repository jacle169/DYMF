using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DYMobileFirst
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BundleTable.EnableOptimizations = true;//是否启用优化
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            //Exception lastError = Server.GetLastError();
            //if (lastError != null)
            //{
            //    Response.StatusCode = 404;
            //    Response.Redirect("~/Home/notfind");
            //    Server.ClearError();
            //}
        }

    }
}
