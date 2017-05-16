using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace testORM.EF
{
    public interface IRepositoryQuery
    {
        IEnumerable<T> List<T>() where T : class;
        IEnumerable<T> List<T>(Expression<Func<T, bool>> predicate) where T : class;
    }
}
