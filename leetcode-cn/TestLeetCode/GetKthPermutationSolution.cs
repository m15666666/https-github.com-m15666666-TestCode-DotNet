using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/permutation-sequence/description/
/// 60.第K个排列
/// 给出集合 [1,2,3,…,n]，其所有元素共有 n! 种排列。
/// 按大小顺序列出所有排列情况，并一一标记，当 n = 3 时, 所有排列如下：
/// </summary>
class GetKthPermutationSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string GetPermutation(int n, int k)
    {
        if ( n < 1 ) return string.Empty;
        if ( n == 1 ) return "1";

        int n1 = 1;
        int count = 1;

        while( n1 < k && count < n )
        {
            count++;
            n1 *= count;
        }

        if (n1 < k) return string.Empty;

        List<int> high = new List<int>();
        List<int> low = new List<int>();

        int i = 0;
        for (; i < n - count; i++) high.Add(i + 1);
        for (; i < n; i++) low.Add(i + 1);

        BackTrade(high, low, k, n1, count);

        return string.Join("", high);
    }

    private static void BackTrade( List<int> high, List<int> low, int k, int n1, int count )
    {
        if (low.Count == 0) return;
        if( low.Count == 1)
        {
            high.Add(low[0]);
            low.Clear();
            return;
        }

        int smallN1 = n1 / count;
        int remainder;
        int quotient = Math.DivRem( k, smallN1, out remainder );
        //int index = (int)Math.Ceiling( k / smallN1 ) - 1;
        int index = ( quotient - 1 ) + (0 < remainder ? 1 : 0);
        if (index < 0 || low.Count <= index ) return;

        high.Add(low[index]);
        low.RemoveAt(index);

        k -= smallN1 * index;
        BackTrade(high, low, k, smallN1, count - 1);
    }
}

/// <summary>
/// 60.第K个排列,别人的答案，试一下
/// </summary>
class GetKthPermutationSolution_Others_1
{
    public static void Test()
    {
        var ret = GetPermutation(3, 2);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public static string GetPermutation(int n, int k)
    {
        if (n == 1)
            return "1";
        int count = GetCount(n - 1);
        Console.WriteLine(count + " " + k);
        int x = (k - 1) / count + 1;
        return replace(GetPermutation(n - 1, k - x * count + count), x);
    }

    private static int GetCount(int n)
    {
        int ret = 1;
        for (int i = 1; i <= n; i++)
            ret *= i;

        return ret;
    }

    private static string replace(string s, int x)
    {
        //Console.WriteLine(s);
        Console.WriteLine(x);
        string ret = "";
        ret += (char)('0' + x);
        foreach (var n in s)
            ret += n - '0' >= x ? (char)(n + 1) : n;

        return ret;
    }
}