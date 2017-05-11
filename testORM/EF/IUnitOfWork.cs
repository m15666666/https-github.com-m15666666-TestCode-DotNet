using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testORM.EF
{
    public interface IUnitOfWork //: IDisposable
    {
        void Commit();
    }
}
