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
/// </summary>
class FractionToRecurringDecimalSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    
    public int ClimbStairs(int n)
    {
        Dictionary<int, int> n2PathCount = new Dictionary<int, int>();
        return ClimbStairs(n, n2PathCount);
    }

    private int ClimbStairs(int n, Dictionary<int, int> n2PathCount)
    {
        if (n <= 1) return 1;
        if (n == 2) return 2;

        if (n2PathCount.ContainsKey(n)) return n2PathCount[n];

        var ret = ClimbStairs(n - 1, n2PathCount) + ClimbStairs(n - 2, n2PathCount);

        if (!n2PathCount.ContainsKey(n)) n2PathCount[n] = ret;

        return ret;
    }
}