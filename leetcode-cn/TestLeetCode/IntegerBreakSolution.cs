using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个正整数 n，将其拆分为至少两个正整数的和，并使这些整数的乘积最大化。 返回你可以获得的最大乘积。

示例 1:

输入: 2
输出: 1
解释: 2 = 1 + 1, 1 × 1 = 1。
示例 2:

输入: 10
输出: 36
解释: 10 = 3 + 3 + 4, 3 × 3 × 4 = 36。
说明: 你可以假设 n 不小于 2 且不大于 58。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/integer-break/
/// 343. 整数拆分
/// https://blog.csdn.net/OneDeveloper/article/details/79958654
/// </summary>
class IntegerBreakSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int IntegerBreak(int n)
    {
        if (n <= 3) return n - 1;
        int[] dp = new int[n + 1];
        dp[1] = 1;
        dp[2] = 2;
        dp[3] = 3;
        for (int i = 4; i <= n; ++i)
        {
            dp[i] = Math.Max(2 * dp[i - 2], 3 * dp[i - 3]);
        }
        return dp[n];
    }
}
/*
public class Solution {
    public int IntegerBreak ( int n ) {
        Trace.Assert ( n >= 2 );
        //memo[i]表示将数字i分割（至少分割两部分）后得到的最大乘积
        int[] memo = Enumerable.Repeat ( -1,n + 1 ).ToArray ();
        memo[1] = 1;
        for ( int i = 0 ;i <=n ;i++ ) {
            //求解memo[i]
            for ( int j = 0 ;j <= i-1 ;j++ ) {
                //j+(i-j)
                memo[i] = _Max ( memo[i],j * ( i - j ),j * memo[i - j] );
            }
        }
        return memo[n];
    }
    public int _Max ( int a,int b,int c ) {
        return Math.Max ( a,Math.Max ( b,c ) );
    }
}
public class Solution {
    public int IntegerBreak(int n) {
            if (n == 2) return 1;
            if(n==3) return 2;
            int yushu = n % 3;
            int threenum = 0;
            int twonum = 0;
            int result = 0;
            if (yushu == 0)
            {
                threenum = n / 3;
            }
            else if (yushu == 1)
            {
                threenum = (n - 4) / 3;
                twonum = 2;
            }
            else if (yushu == 2)
            {
                threenum = n / 3;
                twonum = 1;
            }
            result = Convert.ToInt32(Math.Pow(2, twonum) * Math.Pow(3, threenum));
            return result;
    }
}
public class Solution {
    public int IntegerBreak(int n) {
        if(n>5)
        {
            var count=n/3;
            var r=1;
            var s=n%3;
            if(s==1)
            {
                for(var i=1;i<count;i++)r*=3;
                return r*4;
            }
            else if(s==0)
            {
                for(var i=0;i<count;i++)r*=3;
                return r;
            }
            else
            {
                for(var i=0;i<count;i++)r*=3;
                return r*=2;
            }
        }
        if(n==5)return 6;
        if(n==4)return 4;
        if(n==3)return 2;
        return 1;
    }
}
public class Solution {
    public int IntegerBreak(int n) {
        if (n <= 3)
                return n-1;
            int res = 1;
            while (n > 4)
            {
                res *= 3;
                n -= 3;
            }
            return res * n;
    }
}
public class Solution {
    public int IntegerBreak(int n) {
        if (n <= 3) {
            return n - 1;
        }
        if (n == 4) {
            return 4;
        }
        // 若n-3<=3，停止递归，又因为4可以拆为2、2,4=2*2，所以若n-3为4，也可以停止递归。
        if (n - 3 <= 4) {
            return 3 * (n - 3);
        }
        return 3 * IntegerBreak(n - 3);
    }
}
*/
