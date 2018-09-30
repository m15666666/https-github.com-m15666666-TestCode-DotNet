using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/divide-two-integers/
/// 两数相除
/// 给定两个整数，被除数 dividend 和除数 divisor。将两数相除，要求不使用乘法、除法和 mod 运算符。
/// </summary>
class TwoNumDivideSolution
{
    public static void Test()
    {
        Divide(2147483647, 3);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static int Divide(int dividend0, int divisor0)
    {
        if (dividend0 == 0 || divisor0 == 0) return 0;
        if (divisor0 == 1) return dividend0;
        if (divisor0 == -1) return GetReturn(dividend0, true);

        bool isNegative = false;
        long dividend = dividend0;
        long divisor = divisor0;

        if (0 < dividend && divisor < 0)
        {
            isNegative = true;
            divisor = -divisor;
        }
        else if (0 < divisor && dividend < 0)
        {
            isNegative = true;
            dividend = -dividend;
        }
        if (divisor < 0 && dividend < 0 )
        {
            divisor = -divisor;
            dividend = -dividend;
        }

        if (dividend < divisor) return 0;

        long ret = 0;
        long timesValue;
        long times;

        while (true)
        {
            FastTimes( dividend, divisor, out timesValue, out times);
            if (0 == times) break;

            dividend -= timesValue;
            ret += times;
        }

        return GetReturn(ret, isNegative);
    }

    private static int GetReturn( long input, bool isNegative )
    {
        long ret = isNegative ? -input : input;
        if ( ret < int.MinValue) return int.MinValue;
        if ( int.MaxValue < ret) return int.MaxValue;
        return (int)ret;
    }

    private static void FastTimes( long dividend, long divisor, out long timesValue, out long times )
    {
        timesValue = 0;
        times = 0;
        if (dividend < divisor) return;

        long a = divisor;
        long t = 1;

        a <<= 1;
        while ( a <= dividend )
        {
            t <<= 1;
            a <<= 1;
        }
        a >>= 1;

        timesValue = a;
        times = t;
    }
}