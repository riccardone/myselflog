﻿using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using MySelf.Diab.Core.Contracts;
using MySelf.Diab.Data;
using MySelf.Diab.Data.Contracts;
using MySelf.Diab.Security;
using MySelf.WebClient.Models;

namespace MySelf.WebClient.App_Start
{
    public class IocConfig
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).OnActivated(e =>
            {
                var viewBag =
                    ((Controller)
                     e.Instance).ViewBag;
                viewBag.Version =
                    Assembly.
                        GetExecutingAssembly
                        ().GetName().
                        Version.ToString(3);
            });

            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>();
            builder.RegisterType<ModelReader>().As<IModelReader>();
            builder.RegisterType<CryptoService>().As<ICryptoService>().InstancePerHttpRequest().InstancePerApiRequest();
            builder.RegisterType<Mapper>().As<IMapper>().InstancePerHttpRequest().InstancePerApiRequest();
            builder.RegisterType<LogManager>().As<ILogManager>().InstancePerHttpRequest().InstancePerApiRequest();

            builder.RegisterFilterProvider();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}