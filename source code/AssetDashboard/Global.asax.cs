using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace StarTrack.Dashboard {

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {

            //if (HttpContext.Current.Session != null)
            //{

            //    var handler = (MvcHandler)Context.Handler;
            //    var routeData = handler != null ? handler.RequestContext.RouteData : null;
            //    var routeLang = routeData != null ? routeData.Values["Language"].ToString() : null;

            //    if (!string.IsNullOrEmpty(routeLang))
            //        if (routeLang == "ar" && (handler.RequestContext.HttpContext.Request.Url.ToString().IndexOf("/ar") < handler.RequestContext.HttpContext.Request.Url.ToString().IndexOf("/en") || (handler.RequestContext.HttpContext.Request.Url.ToString().IndexOf("/ar") != -1 && handler.RequestContext.HttpContext.Request.Url.ToString().IndexOf("/en") == -1)))
            //        {
            //            this.Session["M_LANGUAGE"] = "ar-SA";
            //            if (this.Request.Cookies != null)
            //                this.Request.Cookies.Set(new HttpCookie("M_LANGUAGE", "ar-SA"));
            //            handler.RequestContext.RouteData.Values["Language"] = "ar";
            //        }
            //        else
            //            if (routeLang == "en" && (handler.RequestContext.HttpContext.Request.Url.ToString().IndexOf("/en") < handler.RequestContext.HttpContext.Request.Url.ToString().IndexOf("/ar") || (handler.RequestContext.HttpContext.Request.Url.ToString().IndexOf("/en") != -1 && handler.RequestContext.HttpContext.Request.Url.ToString().IndexOf("/ar") == -1)))
            //        {
            //            this.Session["M_LANGUAGE"] = "en-GB";
            //            if (this.Request.Cookies != null)
            //                this.Request.Cookies.Set(new HttpCookie("M_LANGUAGE", "en-GB"));
            //            handler.RequestContext.RouteData.Values["Language"] = "en-GB";
            //        }


            //    if (this.Session["M_LANGUAGE"] == null)
            //        if (this.Request.Cookies != null && this.Request.Cookies["M_LANGUAGE"] != null)
            //            this.Session["M_LANGUAGE"] = this.Request.Cookies["M_LANGUAGE"].Value;
            //        else
            //            this.Session["M_LANGUAGE"] = "ar-SA";

            //    var ci = new CultureInfo(this.Session["M_LANGUAGE"].ToString());

            //    handler.RequestContext.RouteData.Values["Language"] = this.Session["M_LANGUAGE"].ToString() == "ar-SA" ? "ar" : "en";

            //    if (ci == null)
            //    {
            //        string langName = this.Request.Cookies["M_LANGUAGE"] == null ? "ar-SA" : this.Request.Cookies["M_LANGUAGE"].Value;

            //        if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
            //        {

            //            langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
            //        }

            //        ci = new CultureInfo(langName);
            //        this.Session["M_LANGUAGE"] = langName;
            //    } 
            var ci = new CultureInfo("en");
            ci.DateTimeFormat = new CultureInfo("en-GB").DateTimeFormat;
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            Thread.CurrentThread.CurrentCulture.DateTimeFormat = ci.DateTimeFormat;
            //}
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            //Helper.Logger.Exception(ex, "Application_Error");
            if (ex is HttpAntiForgeryException)
            {
                var oldUrl = Request.UrlReferrer;
                Response.Clear();
                Server.ClearError();
                Response.Redirect(oldUrl.ToString(), true);
            }
        }

    }

}
