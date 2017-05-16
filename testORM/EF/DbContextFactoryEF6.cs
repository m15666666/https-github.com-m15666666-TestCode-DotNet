using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testORM.EF
{
    public class DbContextFactoryEF6 : IDbContextFactory
    {
        public Func<DbContext> GetDbContextHandler { get; set; }

        DbContext IDbContextFactory.GetDbContext()
        {
            if(GetDbContextHandler != null)
            {
                return GetDbContextHandler();
            }

            return null;
        }

        

    }
}
