using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
你的任务是计算 ab 对 1337 取模，a 是一个正整数，b 是一个非常大的正整数且会以数组形式给出。

示例 1:

输入: a = 2, b = [3]
输出: 8
示例 2:

输入: a = 2, b = [1,0]
输出: 1024
*/
/// <summary>
/// https://leetcode-cn.com/problems/super-pow/
/// 372. 超级次方
/// https://blog.csdn.net/zrh_CSDN/article/details/83932992
/// </summary>
class SuperPowSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int SuperPow(int a, int[] b)
    {
        if (a == 0) return 0;
        if (b == null || b.Length == 0) return 1;

        long res = 1;
        for (int i = 0; i < b.Length; ++i)
        {
            res = Pow(res, 10) * Pow(a, b[i]) % 1337;
        }
        return (int)res;
    }
    private long Pow(long x, int n)
    {
        if (n == 0) return 1;
        if (n == 1) return x % 1337;
        return Pow(x % 1337, n / 2) * Pow(x % 1337, n - n / 2) % 1337;
    }
}