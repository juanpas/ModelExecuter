using BuildingBlock.Repository;
using BuildingBlock.Repository.Contracts;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BuildingBlock.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var kernel = new StandardKernel(); // Ninject IoC

            kernel.Bind<RepositoryFactories>().To<RepositoryFactories>().InSingletonScope();
            kernel.Bind<IRepositoryProvider>().To<RepositoryProvider>();
            kernel.Bind<IMainUow>().To<MainUow>();

            kernel.Bind<Utils.Utils>().To<Utils.Utils>();


            NinjectDependencyResolver dependencyResolver = new NinjectDependencyResolver(kernel);

            DependencyResolver.SetResolver(dependencyResolver);

            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;


            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
