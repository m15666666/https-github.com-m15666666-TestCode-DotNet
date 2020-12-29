using System;

namespace TestLeetCode.BasicMath
{
    /// <summary>
    /// 牛顿迭代法计算
    /// </summary>
    public static class Newton
    {
        public static void Test()
        {
            if (!IsPerfectSquare(16)) throw new Exception();
            if (IsPerfectSquare(17)) throw new Exception();
            if (Sqrt(16) != 4) throw new Exception();
            if (Sqrt(15) != 3) throw new Exception();
        }

        /// <summary>
        /// 计算是否为平方数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsPerfectSquare(int num)
        {
            if (num < 2) return true;
            int x = Sqrt(num);
            //long x = num / 2; // x 从一半开始计算
            //while (num < x * x) x = (x + num / x) / 2;
            return x * x == num;
        }

        /// <summary>
        /// 计算平方根
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int Sqrt(int num)
        {
            if (num <= 0) return 0;
            if (num < 4) return 1;
            return (int)Sqrt((double)num);
        }

        /// <summary>
        /// 计算平方根
        /// y = f(x) = x^2 - num，当 y == 0 时，使用牛顿切线法求 x 的值
        /// (x^2 - num) / (x - x?) = 2x
        /// (x^2 - num) / 2x = x - x?
        /// x? = x - (x^2 - num) / 2x = (2x^2 - x^2 + num) / 2x = (x^2 + num) / 2x = 1/2 * ( x + num / x)
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static double Sqrt(double num, double eps = 1e-7)
        {
            if (num <= 0) return 0;

            double x0 = num;
            while (true)
            {
                double x1 = 0.5 * (x0 + num / x0);
                if (Math.Abs(x0 - x1) < eps) break;
                x0 = x1;
            }
            return x0;
        }
    }
}