using DYMobileFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Principal;
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

            //BundleTable.EnableOptimizations = true;//是否启用优化

            using (var dbcontext = new BookDBContext())
            {
                var objectContext = ((IObjectContextAdapter)dbcontext).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }  //对程序中定义的所有DbContext逐一进行这个操作

            //自定义日期类型错误提示
            ModelBinders.Binders.Add(typeof(DateTime), new MyDateTimeModelBinder());
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

        protected void Application_End(object sender, EventArgs e)
        {
            try
            {
                using (var wc = new WebClient())
                {
                    var url = Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, "");
                    url = url + "/alive.aspx";
                    wc.DownloadString(url);
                }
            }
            catch (Exception)
            {
                
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Context.User != null)
            {
                using (var dbcontext = new BookDBContext())
                {
                    var state = dbcontext.SystemUsers.FirstOrDefault(o => o.ot_userId == Context.User.Identity.Name);
                    if (state != null)
                    {
                        GenericPrincipal gp = new GenericPrincipal(Context.User.Identity, new string[] { state.ot_skin.ToString() });
                        Context.User = gp;
                    }
                }
            }
        }

    }

    public class MyDateTimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (!string.IsNullOrEmpty(displayFormat) && value != null)
            {
                DateTime date;
                displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
                // use the format specified in the DisplayFormat attribute to parse the date
                if (DateTime.TryParseExact(value.AttemptedValue, displayFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                {
                    bindingContext.ModelState.AddModelError(
                        bindingContext.ModelName,
                        string.Format("{0} 错误的日期格式", value.AttemptedValue)
                    );
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
