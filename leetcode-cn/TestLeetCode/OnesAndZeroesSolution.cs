using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
在计算机界中，我们总是追求用有限的资源获取最大的收益。

现在，假设你分别支配着 m 个 0 和 n 个 1。另外，还有一个仅包含 0 和 1 字符串的数组。

你的任务是使用给定的 m 个 0 和 n 个 1 ，找到能拼出存在于数组中的字符串的最大数量。每个 0 和 1 至多被使用一次。

注意:

给定 0 和 1 的数量都不会超过 100。
给定字符串数组的长度不会超过 600。
示例 1:

输入: Array = {"10", "0001", "111001", "1", "0"}, m = 5, n = 3
输出: 4

解释: 总共 4 个字符串可以通过 5 个 0 和 3 个 1 拼出，即 "10","0001","1","0" 。
示例 2:

输入: Array = {"10", "0", "1"}, m = 1, n = 1
输出: 2

解释: 你可以拼出 "10"，但之后就没有剩余数字了。更好的选择是拼出 "0" 和 "1" 。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/ones-and-zeroes/
/// 474. 一和零
/// https://blog.csdn.net/koukehui0292/article/details/84425574
/// https://zhuanlan.zhihu.com/p/50732119
/// http://www.dongcoder.com/detail-894561.html
/// </summary>
class OnesAndZeroesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindMaxForm(string[] strs, int m, int n)
    {
        int[,] dp = new int[m + 1,n+1];
        
        foreach (string str in strs)
        {
            int zeros = str.Count(c => c == '0');
            int ones = str.Length - zeros;

            for (int i = m; i >= zeros; --i)
	        {
		        for (int j = n; j >= ones; --j)
		        {
			        dp[i,j] = Math.Max(dp[i,j], dp[i - zeros,j - ones] + 1);
                }
	        }
        }
        return dp[m,n];
    }
}
/*
public class Solution {
    public int FindMaxForm(string[] strs, int m, int n) {
        int [,] Cost = new int[2, strs.Length];
        
        for(int i = 0; i < strs.Length ; i++)
        {
            foreach(char c in strs[i])
            {
                Cost[c == '0' ? 0 : 1, i ]++;
            }
        }
        
        int [,] package = new int [m + 1, n + 1];
        
        void FindMax(int x, int y, int i)
        {
            if(x >= Cost[0, i - 1] && y >= Cost[1, i - 1])
                package[x, y] = Math.Max(package[x, y], package[x - Cost[0, i - 1], y - Cost[1, i - 1]] + 1);
        }
        
        for(int i = 1; i < strs.Length; i++)
        {
            for(int M = m; M >= 0; M--)
            {
                for(int N = n; N >= 0; N--)
                {
                    FindMax(M, N, i);
                }
            }
        }
        
        FindMax(m , n, strs.Length);
        
        return package[m,n];
    }
} 
*/
