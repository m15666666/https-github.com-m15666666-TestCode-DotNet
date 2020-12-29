using System;

namespace TestLeetCode.BasicMath
{
    /// <summary>
    /// 最大公约数、最小公倍数
    /// 参考：https://www.csharpstar.com/c-program-to-find-gcd-and-lcm/
    /// </summary>
    public static class GcdLcm
    {
        public static void Test()
        {
            if (GCD(8, 4) != 4) throw new Exception();
            if (GCD(6, 4) != 2) throw new Exception();
            if (GCD(-6, 4) != -2) throw new Exception();
            if (GCD(-6, 6) != -6) throw new Exception();
            if (GCD(6, -6) != -6) throw new Exception();
            if (LCM(8, 4) != 8) throw new Exception();
            if (LCM(6, 4) != 12) throw new Exception();
        }

        /// <summary>
        /// 最大公约数
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static int GCD(int num1, int num2)
        {
            if (num1 == 0 && num2 == 0) return 1;
            if (num1 == 0) return num2;
            if (num2 == 0) return num1;
            if (num1 < num2)
            {
                int t = num1;
                num1 = num2;
                num2 = t;
            }

            int mod;
            while (num2 != 0)
            {
                mod = num1 % num2;
                num1 = num2;
                num2 = mod;
            }
            return num1;
            //return GCD(num2, num1 % num2);
        }

        /// <summary>
        /// 最小公倍数
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static int LCM(int num1, int num2) => (num1 * num2) / GCD(num1, num2);
    }
}