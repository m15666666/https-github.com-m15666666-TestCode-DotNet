using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data.Entity;

namespace testORM.EF
{
    public class RepositoryEFImpl : IRepository, IUnitOfWork
    {
        public DbContext DbContext { get; set; }

        private DbSet<T> GetDbSet<T>() where T : class
        {
            return DbContext.Set<T>();
        }

        public void Add<T>( T entity) where T : class
        {
            if( entity != null )
            {
                GetDbSet<T>().Add( entity );
            }
        }

        public T GetByKeys<T>(params object[] keys ) where T : class
        {
            return GetDbSet<T>().Find(keys);
        }

        public void Update<T>( T entity ) where T : class
        {
            if( entity != null )
            {
                DbContext.Entry( entity ).State = EntityState.Modified;
            }
        }

        public void Delete<T>(T entity) where T : class
        {
            if( entity != null )
            {
                GetDbSet<T>().Remove( entity );
            }
        }

        public void DeleteByKeys<T>( params object[] keys ) where T : class
        {
            var entity  = GetByKeys<T>( keys );
            Delete( entity );
        }

        void IUnitOfWork.Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
