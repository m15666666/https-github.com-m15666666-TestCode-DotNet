using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/fraction-to-recurring-decimal/
/// 166. 分数到小数
/// 给定两个整数，分别表示分数的分子 numerator 和分母 denominator，以字符串形式返回小数。
/// 如果小数部分为循环小数，则将循环的部分括在括号内。
/// 示例 1:
/// 输入: numerator = 1, denominator = 2
/// 输出: "0.5"
/// 示例 2:
/// 输入: numerator = 2, denominator = 1
/// 输出: "2"
/// 示例 3:
/// 输入: numerator = 2, denominator = 3
/// 输出: "0.(6)"
/// http://www.cnblogs.com/leo-lzj/p/9613643.html
/// </summary>
class FractionToRecurringDecimalSolution
{
    public static void Test()
    {
        var ret = FractionToDecimal(7, -12);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static string FractionToDecimal(int numerator, int denominator)
    {
        if (numerator == 0) return "0";

        long nu = numerator;
        long de = denominator;

        StringBuilder sb = new StringBuilder();
        var isNegative = nu < 0 ^ de < 0;
        if (isNegative) sb.Append("-");

        if (nu < 0) nu = -nu;
        if (de < 0) de = -de;

        // 整数部分
        sb.Append(nu / de);

        var remainder = nu % de;
        if ( remainder == 0 ) return sb.ToString();

        sb.Append(".");

        Dictionary<long, int> remainders = new Dictionary<long, int>();

        // 记录当前添加的小数在答案中的位置
        int decimalPosition = sb.Length;

        while ( remainder != 0 )
        {
            // 若重复出现余数，则证明出现循环
            if ( remainders.ContainsKey(remainder) )
            {
                sb.Insert(remainders[remainder], "(");
                sb.Append(")");
                return sb.ToString();
            }

            // 记录余数出现的位置，顺便递增pos
            remainders[remainder] = decimalPosition++;

            long currentNumerator = 10 * remainder;
            sb.Append( currentNumerator / de );
            remainder = currentNumerator % de;
        }

        return sb.ToString();
    }
}