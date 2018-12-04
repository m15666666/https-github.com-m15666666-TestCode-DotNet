using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace testORM.EF
{
    public class RepositoryEFImpl : IRepository, IUnitOfWork
    {
        #region 注入 DbContext

        public DbContext DbContext { get {
                if(_dbContext == null && DbContextFactory != null)
                {
                    return (_dbContext = DbContextFactory.GetDbContext());
                }
                return _dbContext;
            }
            set { _dbContext = value; }
        }

        private DbContext _dbContext;

        private IDbContextFactory DbContextFactory { get; set; }

        public RepositoryEFImpl()
        {
        }

        public RepositoryEFImpl( IDbContextFactory dbContextFactory )
        {
            DbContextFactory = dbContextFactory;
        }

        #endregion

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

        public void AddOrUpdate<T>(T entity) where T : class
        {
            if (entity != null)
            {
                GetDbSet<T>().AddOrUpdate(entity);
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
