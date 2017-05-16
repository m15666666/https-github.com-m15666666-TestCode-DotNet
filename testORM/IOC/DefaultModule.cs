using Autofac;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testORM.EF;

namespace testORM.IOC
{
    public class DefaultModule : Module
    {
        private const string DbContextKey = "MainDbContextKey";
        private const string connectionString = "data source=10.3.2.188;initial catalog=testORM;persist security info=True;user id=sa;password=#db877350;MultipleActiveResultSets=True;App=EntityFramework";

        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<CharacterRepository>().As<ICharacterRepository>();
            //builder.RegisterType<Model2>().Named<DbContext>(DbContextKey);
            builder.Register<DbContext>(context => {
                return new Model2(connectionString);
            }).Named<DbContext>(DbContextKey).InstancePerDependency();

            builder.Register<IDbContextFactory>( context => {
                IComponentContext c = context.Resolve<IComponentContext>();
                return new DbContextFactoryEF6()
                {
                    GetDbContextHandler = () => c.ResolveNamed<DbContext>(DbContextKey)
                };
            }).SingleInstance();

            builder.RegisterType<RepositoryEFImpl>().As<IRepository>().InstancePerDependency();

            builder.RegisterType<RepositoryQueryEFImpl>().As<IRepositoryQuery>().InstancePerDependency();
        }
    }
}
