using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testORM.EF;

namespace testORM
{
    class Program
    {
        static void Main(string[] args)
        {
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
    }
}
