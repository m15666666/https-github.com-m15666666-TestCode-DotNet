using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testORM.EF;
using testORM.IOC;

namespace testORM
{
    class Program
    {
        static void Main(string[] args)
        {
            UseAutofac();
            return;
            // 下面是没有使用IOC容器的代码

            var repositoryEfImpl = new RepositoryEFImpl();
            repositoryEfImpl.DbContext = new Model2();

            IRepository repository = repositoryEfImpl;
            IUnitOfWork unitOfWork = ( repository as IUnitOfWork );

            int personID1 = 1;
            repository.DeleteByKeys<BS_Person>(personID1);
            unitOfWork.Commit();

            BS_Person p1 = new BS_Person {Id_NR = personID1, Name_TX = "P1-张"};
            repository.Add( p1 );

            unitOfWork.Commit();

            p1 = repository.GetByKeys<BS_Person>( personID1 );
            p1.Description_TX = "张三";
            repository.Update( p1);

            unitOfWork.Commit();

            //Console.WriteLine( "Press enter to exit." );
            //Console.ReadLine();
        }

        private static IContainer Container { get; set; }
        private static void UseAutofac()
        {
            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DefaultModule>();
            var container = containerBuilder.Build();

            Container = container;

            var repository = Container.Resolve<IRepository>();
            var repositoryQuery = Container.Resolve<IRepositoryQuery>();
            TestRepository(repository, repositoryQuery, (repository as IUnitOfWork));

            repository = Container.Resolve<IRepository>();
            repositoryQuery = Container.Resolve<IRepositoryQuery>();
            TestRepository(repository, repositoryQuery, (repository as IUnitOfWork));

            Container.Dispose();
        }

        private static void TestRepository(IRepository repository, IRepositoryQuery repositoryQuery, IUnitOfWork unitOfWork)
        {
            int personID1 = 1;
            repository.DeleteByKeys<BS_Person>(personID1);
            unitOfWork.Commit();

            BS_Person p1 = new BS_Person { Id_NR = personID1, Name_TX = "P1-张" };
            repository.Add(p1);

            unitOfWork.Commit();

            var list1 = repositoryQuery.List<BS_Person>(p => p.Name_TX.Contains("1")).ToList();
            var list2 = repositoryQuery.List<BS_Person>(p => p.Name_TX.Contains("2")).ToList();

            p1 = repository.GetByKeys<BS_Person>(personID1);
            p1.Description_TX = "张三";
            repository.Update(p1);

            unitOfWork.Commit();
        }
    }
}
