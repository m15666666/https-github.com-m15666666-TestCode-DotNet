using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/powx-n/
/// 50. Pow(x,n)
/// 实现 pow(x, n) ，即计算 x 的 n 次幂函数。
/// </summary>
class MyPowSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public double MyPow(double x, int n)
    {
        if (x == 0) return 0;
        if (n == 0) return 1;
        if (n == 1) return x;
        if (n == -1) return 1/x;

        Dictionary<long, double> pow2v = new Dictionary<long, double>();
        long n1 = n;
        if( n1 < 0)
        {
            x = 1.0 / x;
            n1 = -n1;
        }

        double x1 = x;
        long pow = 1;

        pow2v[pow] = x1;
        while ( pow < n1 )
        {
            x1 = x1 * x1;
            pow *= 2;
            pow2v[pow] = x1;
        }

        if (pow == n1) return x1;

        double ret = 1;
        while( 0 < n1 )
        {
            if( pow <= n1 )
            {
                ret *= x1;

                n1 -= pow;
                continue;
            }

            pow /= 2;
            x1 = pow2v[pow];
        }

        return ret;
    }
}