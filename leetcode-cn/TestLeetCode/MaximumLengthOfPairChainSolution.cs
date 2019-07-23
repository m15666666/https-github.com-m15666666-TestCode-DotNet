using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出 n 个数对。 在每一个数对中，第一个数字总是比第二个数字小。

现在，我们定义一种跟随关系，当且仅当 b < c 时，数对(c, d) 才可以跟在 (a, b) 后面。我们用这种形式来构造一个数对链。

给定一个对数集合，找出能够形成的最长数对链的长度。你不需要用到所有的数对，你可以以任何顺序选择其中的一些数对来构造。

示例 :

输入: [[1,2], [2,3], [3,4]]
输出: 2
解释: 最长的数对链是 [1,2] -> [3,4]
注意：

给出数对的个数在 [1, 1000] 范围内。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-length-of-pair-chain/
/// 646. 最长数对链
/// https://www.jianshu.com/p/52a0d972d02d
/// </summary>
class MaximumLengthOfPairChainSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindLongestChain(int[][] pairs)
    {
        if (pairs == null || pairs.Length == 0) return 0;

        Comparison<int[]> handler = (x, y) => x[1].CompareTo(y[1]);
        Array.Sort(pairs, handler);

        int ret = 1;
        var rear = pairs[0][1];
        for( int i = 1; i < pairs.Length; i++)
        {
            var pair = pairs[i];
            if(rear < pair[0])
            {
                ret++;
                rear = pair[1];
            }
        }
        return ret;
    }
}
/*
public class Solution {
   public int FindLongestChain(int[][] pairs)
        {
            var p = pairs.ToList();
            p.Sort((a, b) => a[1] - b[1]);
            int c = 0;
            int pre = int.MinValue;
            for (int i = 0; i < pairs.Length; i++)
            {
                if (p[i][0]>pre)
                {
                    pre = p[i][1];
                    c++;
                }
            }
            return c;
        }
}
public class Solution {
    public class MyComparer : IComparer<int[]>
    {
        public int Compare(int[] x, int[] y)
        {
            if (x[0] > y[0])
            {
                return 1;
            }
            else if (x[0] < y[0])
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
    public int FindLongestChain(int[][] pairs) {
        Array.Sort<int[]>(pairs,new MyComparer());   
        int[] dp=new int[pairs.Length];
        dp[0]=1;
        for(int j=1;j<pairs.Length;j++)
        {
            dp[j]=1;
            for(int i=0;i<j;i++)
            {
                if(pairs[j][0]>pairs[i][1])
                {
                    dp[j]=Math.Max(dp[j],dp[i]+1);
                }
            }
        }
        
        int max=int.MinValue; 
        for(int i=0;i<dp.Length;i++)
        {
            if(dp[i]>max)
                max=dp[i];
        }
        return max;
    }
}
*/
