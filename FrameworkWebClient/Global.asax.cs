using GenericFramework.SystemCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FrameworkWebClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        CoreManager manager;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            startup();
            FrameworkWebClient.SystemConfig.coreManger = manager;
        }

        void startup()
        {
            manager = new CoreManager();//use plugin in prod
            manager.CommandParamPrefix = "@";//from config
            manager.ConnectionString = @"Server = NEPTUNELABS\SQLEXPRESS2K8;database = FrameworkTest;integrated security = true;";//from config
            manager.DbConnctivity = DbConnectivity.AdoDotNet;//from config
            manager.DbContext = new GenericFramework.DbContext.DbContext();//use plugin
            manager.DbContextAPI = new GenericFramework.DbContextAPI.DbContextAPI();//use plugin
            manager.DbEngine = DbEngine.MSSQLServer;//from config
            manager.DefaultLanguage = Language.English;//from db or config
            manager.GenericDataMapper = new GenericFramework.GenericDataMapUtil.GenericDataMapUtility();//use plugin
            manager.GenericFrameworkError = new GenericFramework.SystemCore.GenericFrameworkError();//plugin
            manager.GenericFrameworkApplicationError = new GenericFramework.SystemCore.GenericFrameworkApplicationError();//use plugin
            manager.GenericFrameworkExceptionHandler = new GenericFrameworkException();//plugin
            manager.InsertConnector = new GenericFramework.Connectors.InsertConnector();//plugin
            manager.InsertGateway = new GenericFramework.Gateway.InsertGateway();//plugin

            manager.DbContextAPI.SystemCoreManager = manager;
            manager.GenericDataMapper.SystemCoreManager = manager;
            manager.InsertGateway.SystemCoreManager = manager;
            manager.InsertConnector.SystemCoreManager = manager;
        }
    }
}
