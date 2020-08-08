using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个整数，分别表示分数的分子 numerator 和分母 denominator，以字符串形式返回小数。

如果小数部分为循环小数，则将循环的部分括在括号内。

示例 1:

输入: numerator = 1, denominator = 2
输出: "0.5"
示例 2:

输入: numerator = 2, denominator = 1
输出: "2"
示例 3:

输入: numerator = 2, denominator = 3
输出: "0.(6)"
 
*/
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
/*

分数到小数
力扣 (LeetCode)
发布于 2019-06-27
15.3k
概要
这是一道非常直观的代码题，但需要考虑很多细节。

要点
不需要复杂的数学知识，只需要数学的基本知识。了解长除法的运算规则。
使用长除法计算 \dfrac{4}{9} 
9
4
​	
 ，循环节很显然就会找到。那么计算 \dfrac{4}{333} 
333
4
​	
  呢，能找到规律吗？
注意边界情况！列出所有你可以想到的测试数据并验证你的代码。
方法：长除法
思路

核心思想是当余数出现循环的时候，对应的商也会循环。

\begin{array}{rll} 0.16 \\ 6{\overline{\smash{\big)}\,1.00}} \\[-1pt] \underline{0\phantom{.00}} \\[-1pt] 1\phantom{.}0 \phantom{0} && \Leftarrow \textrm{余数为 1，标记 1 出现在位置 0} \\[-1pt] \underline{\phantom{0}6\phantom{0}} \\[-1pt] \phantom{0}40 && \Leftarrow \textrm{余数为 4，标记 4 出现在位置 1} \\[-1pt] \underline{\phantom{0}36} \\[-1pt] \phantom{00}4 && \Leftarrow \textrm{余数为 4，在位置 1 出现过，} \\ \phantom{00} && \quad \textrm{所以循环节从位置 1 开始，为 1(6)} \\[-1pt] \end{array}
0.16
6 
)
 1.00
 
0.00
​	
 
1.00
060
​	
 
040
036
​	
 
004
00
​	
  
​	
  
⇐余数为 1，标记 1 出现在位置 0
⇐余数为 4，标记 4 出现在位置 1
⇐余数为 4，在位置 1 出现过，
所以循环节从位置 1 开始，为 1(6)
​	
 

算法

需要用一个哈希表记录余数出现在小数部分的位置，当你发现已经出现的余数，就可以将重复出现的小数部分用括号括起来。

再出发过程中余数可能为 0，意味着不会出现循环小数，立刻停止程序。

就像 两数相除 问题一样，要考虑负分数以及极端情况，比如说 \dfrac{-2147483648}{-1} 
−1
−2147483648
​	
  。

下面列出了一些很好的测试样例：

测试样例	解释
\frac{0}{1} 
1
0
​	
 	被除数为 0。
\frac{1}{0} 
0
1
​	
 	除数为 0，应当抛出异常，这里为了简单起见不考虑。
\frac{20}{4} 
4
20
​	
 	答案是整数，不包括小数部分。
\frac{1}{2} 
2
1
​	
 	答案是 0.5，是有限小数。
\frac{-1}{4} 
4
−1
​	
  or \frac{1}{-4} 
−4
1
​	
 	除数被除数有一个为负数，结果为负数。
\frac{-1}{-4} 
−4
−1
​	
 	除数和被除数都是负数，结果为正数。
\frac{-2147483648}{-1} 
−1
−2147483648
​	
 	转成整数时注意可能溢出。

public String fractionToDecimal(int numerator, int denominator) {
    if (numerator == 0) {
        return "0";
    }
    StringBuilder fraction = new StringBuilder();
    // If either one is negative (not both)
    if (numerator < 0 ^ denominator < 0) {
        fraction.append("-");
    }
    // Convert to Long or else abs(-2147483648) overflows
    long dividend = Math.abs(Long.valueOf(numerator));
    long divisor = Math.abs(Long.valueOf(denominator));
    fraction.append(String.valueOf(dividend / divisor));
    long remainder = dividend % divisor;
    if (remainder == 0) {
        return fraction.toString();
    }
    fraction.append(".");
    Map<Long, Integer> map = new HashMap<>();
    while (remainder != 0) {
        if (map.containsKey(remainder)) {
            fraction.insert(map.get(remainder), "(");
            fraction.append(")");
            break;
        }
        map.put(remainder, fraction.length());
        remainder *= 10;
        fraction.append(String.valueOf(remainder / divisor));
        remainder %= divisor;
    }
    return fraction.toString();
}
下一篇：模拟除法 + 余数循环

public class Solution {
    public string FractionToDecimal(int numerator, int denominator) {
        Hashtable record = new Hashtable();
        IList<long> result = new List<long>();
        long integerPart = 0;
        long calResult = 0;
        int startPlace = 0;
        string str = "";
        long longNumerator = numerator;
        long longDenominator = denominator;

        // cal sig
        long sig = 0;
        if (longNumerator >= 0 && longDenominator > 0 || longNumerator <= 0 && longDenominator < 0)
        {
            sig = 1;
        }
        else
        {
            sig = -1;
        }
        longNumerator = longNumerator < 0 ? -longNumerator : longNumerator;
        longDenominator = longDenominator < 0 ? -longDenominator : longDenominator;
        
        // cal integer part
        calResult = longNumerator / longDenominator;
        if (calResult != 0)
        {
            integerPart = calResult;
            longNumerator = longNumerator % longDenominator;
        }
        longNumerator *= 10;
        record.Add(longNumerator, startPlace);

        // get decimal
        while (longNumerator != 0)
        {
            calResult = longNumerator / longDenominator;
            longNumerator = longNumerator % longDenominator;
            longNumerator *= 10;
            result.Add(calResult);
            startPlace++;

            if (record.Contains(longNumerator))
            {
                // print str
                str += sig > 0 ? "" : "-";
                str += integerPart.ToString();
                str += ".";
                int beginPlace = (int)record[longNumerator];
                for (int i = 0; i < result.Count; i++)
                {
                    if (i == beginPlace)
                    {
                        str += "(";
                    }
                    str += result[i].ToString();
                }
                str += ")";
                return str;
            }
            else
            {
                record.Add(longNumerator, startPlace);
            }
        }

        // print str
        str += sig > 0 ? "" : "-";
        str += integerPart.ToString();
        if (result.Count == 0)
        {
            return str;
        }

        str += ".";
        for (int i = 0; i < result.Count; i++)
        {
            str += result[i].ToString();
        }
        return str;

    }
}
 
 
 
*/