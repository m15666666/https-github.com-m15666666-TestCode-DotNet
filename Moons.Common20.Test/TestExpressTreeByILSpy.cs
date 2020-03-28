using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Moons.Common20.Test
{
    public class TestExpressTreeByILSpy
    {
        public void Map1()
        {
            DataA a = new DataA { Id = 1 };
            Expression<Func<DataA, DataADto>> expressTree = (x) => new DataADto { Id = x.Id };
            var handler = expressTree.Compile();
            var second = handler(a);
        }

        //public void Map2()
        //{
        //    DataA a = new DataA { Id = 1 };
        //    DataADto b = new DataADto();
        //    //Expression<Func<DataA, DataADto, DataADto>> expressTree = (x, y) => new DataADto { Id = y.Id = x.Id };

        //    Expression.
        //    Expression<Action<DataA, DataADto>> expressTree = (x, y) => y.Id = x.Id;
        //    var handler = expressTree.Compile();
        //    handler(a, b);
        //}
    }

    public class DataA
    {
        public int Id { get; set; }
    }

    public class DataADto
    {
        public int Id { get; set; }
    }
}
