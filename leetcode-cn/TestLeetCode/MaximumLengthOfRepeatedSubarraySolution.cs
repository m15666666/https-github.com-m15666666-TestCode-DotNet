using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给两个整数数组 A 和 B ，返回两个数组中公共的、长度最长的子数组的长度。

示例 1:

输入:
A: [1,2,3,2,1]
B: [3,2,1,4,7]
输出: 3
解释: 
长度最长的公共子数组是 [3, 2, 1]。
说明:

1 <= len(A), len(B) <= 1000
0 <= A[i], B[i] < 100 
*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-length-of-repeated-subarray/
/// 718. 最长重复子数组
/// https://blog.csdn.net/xuxuxuqian1/article/details/80896461
/// https://blog.csdn.net/L1558198727/article/details/95106737
/// </summary>
class MaximumLengthOfRepeatedSubarraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindLength(int[] A, int[] B)
    {
        if (A == null || B == null) return 0;

        int m = A.Length;
        int n = B.Length;
        int[,] dp = new int[m + 1, n + 1];
        for (int i = 0; i <= m; i++) dp[i, 0] = 0;
        for (int i = 0; i <= n; i++) dp[0,i] = 0;

        int ret = 0;
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (A[i - 1] == B[j - 1])
                {
                    var v = dp[i - 1, j - 1] + 1;
                    dp[i,j] = v;
                    if (ret < v) ret = v;
                }
                else
                    dp[i,j] = 0; // 要求连续所以置零，不要求连续则为:dp[i - 1, j - 1].
            }
        }
        return ret;
    }
}
/*
public class Solution {
    public int FindLength(int[] A, int[] B) {
        int n1 = A.Length;
        int n2 = B.Length;
        
        int res = 0;
        for (int i = 0; i < n1; i ++) {
            int len = 0;
            for (int j = 0; j < n2 && i + j < n1; j ++) {
                if (B[j] == A[i+j]) {
                    len ++;
                    res = Math.Max(res, len);
                } else {
                    len = 0;
                }
            }
        }
        
        for (int i = 0; i < n2; i ++) {
            int len = 0;
            for (int j = 0; j < n1 && i + j < n2; j ++) {
                if (A[j] == B[i+j]) {
                    len ++;
                    res = Math.Max(res, len);
                } else {
                    len = 0;
                }
            }
        }
        
        return res;
    }
}
public class Solution {
    public int FindLength(int[] A, int[] B)
    {
        int max = 0;
        int[,] dp = new int[A.Length, B.Length];
        for (int i = 0; i < A.Length; i++)
            if (A[i] == B[0])
                dp[i, 0] = 1;
        for (int j = 0; j < B.Length; j++)
            if (A[0] == B[j])
                dp[0, j] = 1;
        for (int i=1; i < A.Length; i++)
        {
            for (int j = 1; j < B.Length; j++)
            {
                if (A[i] == B[j])
                {
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                    if (max < dp[i, j])
                        max = dp[i, j];
                }
            }
        }
        return max;
    }
}
*/
