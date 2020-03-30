using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个整数，被除数 dividend 和除数 divisor。将两数相除，要求不使用乘法、除法和 mod 运算符。

返回被除数 dividend 除以除数 divisor 得到的商。

整数除法的结果应当截去（truncate）其小数部分，例如：truncate(8.345) = 8 以及 truncate(-2.7335) = -2

 

示例 1:

输入: dividend = 10, divisor = 3
输出: 3
解释: 10/3 = truncate(3.33333..) = truncate(3) = 3
示例 2:

输入: dividend = 7, divisor = -3
输出: -2
解释: 7/-3 = truncate(-2.33333..) = -2
 

提示：

被除数和除数均为 32 位有符号整数。
除数不为 0。
假设我们的环境只能存储 32 位有符号整数，其数值范围是 [−231,  231 − 1]。本题中，如果除法结果溢出，则返回 231 − 1。
*/
/// <summary>
/// https://leetcode-cn.com/problems/divide-two-integers/
/// 29. 两数相除
/// 
/// </summary>
class DivideTwoIntegersSolution
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
/*

朴素的想法，没有位运算，没有移位操作
liujin
发布于 4 个月前
15.4k
image.png

越界问题只要对除数是1和-1单独讨论就完事了啊
关于如何提高效率快速逼近结果
举个例子：11 除以 3 。
首先11比3大，结果至少是1， 然后我让3翻倍，就是6，发现11比3翻倍后还要大，那么结果就至少是2了，那我让这个6再翻倍，得12，11不比12大，吓死我了，差点让就让刚才的最小解2也翻倍得到4了。但是我知道最终结果肯定在2和4之间。也就是说2再加上某个数，这个数是多少呢？我让11减去刚才最后一次的结果6，剩下5，我们计算5是3的几倍，也就是除法，看，递归出现了。说得很乱，不严谨，大家看个大概，然后自己在纸上画一画，或者直接看我代码就好啦！

class Solution {
public:
    int divide(int dividend, int divisor) {
        if(dividend == 0) return 0;
        if(divisor == 1) return dividend;
        if(divisor == -1){
            if(dividend>INT_MIN) return -dividend;// 只要不是最小的那个整数，都是直接返回相反数就好啦
            return INT_MAX;// 是最小的那个，那就返回最大的整数啦
        }
        long a = dividend;
        long b = divisor;
        int sign = 1; 
        if((a>0&&b<0) || (a<0&&b>0)){
            sign = -1;
        }
        a = a>0?a:-a;
        b = b>0?b:-b;
        long res = div(a,b);
        if(sign>0)return res>INT_MAX?INT_MAX:res;
        return -res;
    }
    int div(long a, long b){  // 似乎精髓和难点就在于下面这几句
        if(a<b) return 0;
        long count = 1;
        long tb = b; // 在后面的代码中不更新b
        while((tb+tb)<=a){
            count = count + count; // 最小解翻倍
            tb = tb+tb; // 当前测试的值也翻倍
        }
        return count + div(a-tb,b);
    }
};
（有收获的话求个赞..）

public class Solution {
    public int Divide(int dividend, int divisor) {
        int mFlag = dividend>0?-1:1;
        int nFlag = divisor>0?-1:1;
        int flag = -1* mFlag * nFlag; 
        //将值转换为负值进行计算
        int m = dividend * mFlag, n = divisor * nFlag;

        int r = search(m,n);
        if(r == -2147483648 && flag == -1)
        {//只有一种情况溢出，就是最小负值转换为正值时
            return 2147483647;
        }
        
        return r * flag;        
    }
    public int search(int m,int n)
    {
        if(m > n) return 0;//当被除数比除数的绝对值还小的时候结束

        int k = -1, t = n;
        while(m < t && m - t < t)
        {//使用2的幂次进行求解
            t <<= 1;
            k <<= 1;
        }
        return  k + search(m - t,n);
    }
}

public class Solution {
    public int Divide(int dividend, int divisor) {
             bool T = false;
            if ((dividend < 0 && divisor > 0) || (dividend > 0 && divisor < 0))
                T = true;

            if (dividend - divisor == 0) return 1;
            if (dividend + divisor == 0) return -1;
            
            if (dividend > 0)
                dividend = -dividend;
            if (divisor > 0)
                divisor = -divisor;

            int result = 0;
            for (int i = 31; i >= 0; i--)
            {
                if (dividend >> i <= divisor)
                {
                    result += 1 << i;
                    dividend -= divisor << i;
                    result = dividend > 0 ?dividend>-divisor?result-2: result - 1 : result;
                }
            }
              if (T)
                return -result;
            else
                if (result == int.MinValue)
                    return int.MaxValue;
                else
                    return result;
    }
}

 
*/
