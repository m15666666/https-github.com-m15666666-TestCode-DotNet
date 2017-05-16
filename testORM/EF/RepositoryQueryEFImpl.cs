using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace testORM.EF
{
    public class RepositoryQueryEFImpl : IRepositoryQuery
    {
        #region 注入 DbContext

        public DbContext DbContext
        {
            get
            {
                if (_dbContext == null && DbContextFactory != null)
                {
                    return (_dbContext = DbContextFactory.GetDbContext());
                }
                return _dbContext;
            }
            set { _dbContext = value; }
        }

        private DbContext _dbContext;

        private IDbContextFactory DbContextFactory { get; set; }

        public RepositoryQueryEFImpl()
        {
        }

        public RepositoryQueryEFImpl(IDbContextFactory dbContextFactory)
        {
            DbContextFactory = dbContextFactory;
        }

        #endregion

        IEnumerable<T> IRepositoryQuery.List<T>()
        {
            return DbContext.Set<T>().AsEnumerable();
        }

        IEnumerable<T> IRepositoryQuery.List<T>(Expression<Func<T, bool>> predicate)
        {
            return DbContext.Set<T>().Where(predicate).AsEnumerable();
        }
    }
}
