using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Portfolio_Management_Application
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(
          object sender,
          EventArgs e)
        {
            HttpCookie cookie =
                Request.Cookies[
                    FormsAuthentication.FormsCookieName
                ];

            if (cookie == null)
                return;

            FormsAuthenticationTicket ticket =
                FormsAuthentication.Decrypt(cookie.Value);

            if (ticket == null)
                return;

            string[] roles =
                ticket.UserData.Split(',');

            HttpContext.Current.User =
                new System.Security.Principal.GenericPrincipal(
                    new FormsIdentity(ticket),
                    roles
                );
        }
    }
}
