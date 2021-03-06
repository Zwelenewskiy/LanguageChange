using LanguageChange.Models;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LanguageChange
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalHelper.Init();

            /*var error = GlobalHelper.ReadTranslationFiles();
            if (error == null)
            {
                //OK
            }
            else
            {
                //ERROR
            }

            TranslateFileLoadScheduler.Start();*/

            Database.SetInitializer(new UserInitializer());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
