using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个正整数 n，你可以做如下操作：

1. 如果 n 是偶数，则用 n / 2替换 n。
2. 如果 n 是奇数，则可以用 n + 1或n - 1替换 n。
n 变为 1 所需的最小替换次数是多少？

示例 1:

输入:
8

输出:
3

解释:
8 -> 4 -> 2 -> 1
示例 2:

输入:
7

输出:
4

解释:
7 -> 8 -> 4 -> 2 -> 1
或
7 -> 6 -> 3 -> 2 -> 1 
*/
/// <summary>
/// https://leetcode-cn.com/problems/integer-replacement/
/// 397. 整数替换
/// 
/// </summary>
class IntegerReplacementSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int IntegerReplacement(int n)
    {
        Dictionary<long, long> cache = new Dictionary<long, long>
        {
            { 1, 0 },
            { 2, 1 }
        };
        return (int)GetIntegerReplacement(n, cache);
    }

    private long GetIntegerReplacement(long n, Dictionary<long,long> cache )
    {
        if (cache.ContainsKey(n)) return cache[n];

        if (n % 2 == 0) return cache[n] = (1 + GetIntegerReplacement(n / 2, cache));

        return cache[n] = 1 + Math.Min(GetIntegerReplacement(n + 1, cache), GetIntegerReplacement(n - 1, cache));
    }
}
/*
public class Solution {
    public int IntegerReplacement(int n) {
        return Fun(n);
    }
    
    int Fun(long n) {
        if (n == 1) {
            return 0;
        }
        
        if (n%2 == 0) {
            return Fun(n/2) + 1;
        } else {
            int val1 = Fun(n+1);
            int val2 = Fun(n-1);
            return Math.Min(val1, val2) + 1;
        }
    }
}
public class Solution {
    public int IntegerReplacement(long n) {
       return IntegerReplacement2((long)n);
    }
    
    public int IntegerReplacement2(long n) {
         if (n <= 1)
                return 0;
            if (n == 2)
                return 1;

            if(n%2==0)
            {
                return 1 + IntegerReplacement2(n / 2);
            }
            else
            {
                return 1 + Math.Min(IntegerReplacement2(n + 1), IntegerReplacement2(n - 1));
            }
    }
}
*/
