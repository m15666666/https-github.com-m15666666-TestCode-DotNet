using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(WebMVCOwinAutofac.Startup))]

namespace WebMVCOwinAutofac
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888

            //var builder = new ContainerBuilder();
            //// Register dependencies, then...
            //var container = builder.Build();

            //// Register the Autofac middleware FIRST. This also adds
            //// Autofac-injected middleware registered with the container.
            //app.UseAutofacMiddleware(container);

            //// ...then register your other middleware not registered
            //// with Autofac.

            var builder = new ContainerBuilder();

            // STANDARD MVC SETUP:

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Run other optional steps, like registering model binders,
            // web abstractions, etc., then set the dependency resolver
            // to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // OWIN MVC SETUP:

            // Register the Autofac middleware FIRST, then the Autofac MVC middleware.
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
        }
    }
}
