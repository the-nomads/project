using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using SWEN344Project.BusinessInterfaces;
using System.Reflection;
using System.Data.Entity;
using SWEN344Project.Models;
using SWEN344Project.Models.PersistentModels;

[assembly: OwinStartup(typeof(SWEN344Project.Startup))]
namespace SWEN344Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer<CaveWallContext>(new DropCreateDatabaseIfModelChanges<CaveWallContext>());

            var config = new HttpConfiguration();

            RegisterDependencies(config);

            //new AutoFac builder
            var builder = new ContainerBuilder();

            //register dependencies
            builder.RegisterType<FinancialTransactionBusinessObject>()
                            .As<IFinancialTransactionBusinessObject>()
                            .InstancePerLifetimeScope();
            builder.RegisterType<EventBusinessObject>()
                            .As<IEventBusinessObject>()
                            .InstancePerLifetimeScope();
            builder.RegisterType<UserBusinessObject>()
                            .As<IUserBusinessObject>()
                            .InstancePerLifetimeScope();
            builder.RegisterType<WeatherBusinessObject>()
                            .As<IWeatherBusinessObject>()
                            .InstancePerLifetimeScope();
            builder.RegisterType<StockInformationBusinessObject>()
                            .As<IStockInformationBusinessObject>()
                            .InstancePerLifetimeScope();
            builder.RegisterType<PersistenceBusinessObject>()
                            .As<IPersistenceBusinessObject>()
                            .InstancePerLifetimeScope();


            var assembly = typeof(SWEN344Project.Controllers.FinancialTransactionController).Assembly;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var c = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(c);
            config.DependencyResolver = resolver;

            WebApiConfig.Register(config);

            app.UseWebApi(config);

            container = c;
        }

        public static IContainer container;

        private void RegisterDependencies(HttpConfiguration config)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


    }
}
