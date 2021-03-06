﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个元素都是正整数的数组A ，正整数 L 以及 R (L <= R)。

求连续、非空且其中最大元素满足大于等于L 小于等于R的子数组个数。

例如 :
输入: 
A = [2, 1, 4, 3]
L = 2
R = 3
输出: 3
解释: 满足条件的子数组: [2], [2, 1], [3].
注意:

L, R  和 A[i] 都是整数，范围在 [0, 10^9]。
数组 A 的长度范围在[1, 50000]。
*/
/// <summary>
/// https://leetcode-cn.com/problems/number-of-subarrays-with-bounded-maximum/
/// 795. 区间子数组个数
/// https://codeleading.com/article/3658367425/
/// </summary>
class NumberOfSubarraysWithBoundedMaximumSolution
{
    public void Test()
    {
        var ret = NumSubarrayBoundedMax(new int []{ 2, 1, 4, 3 }, 2, 3);
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NumSubarrayBoundedMax(int[] A, int L, int R)
    {
        int len = A.Length;
        int first = 0;
        int count = 0;
        int ret = 0;
        for( int i = 0; i < len; i++)
        {
            var v = A[i];
            if( R < v)
            {
                first = i + 1;
                count = 0;
                continue;
            }
            if (L <= v)
            {
                count = i - first + 1;
                ret += count;
                continue;
            }
            else if (0 < count) ret += count;
        }
        return ret;
    }
}
/*
public class Solution {
    public int NumSubarrayBoundedMax(int[] A, int L, int R) {
            int ans = 0;
            int n = A.Length;
            //先计算小于等于R的个数
            int i = 0, j = 0;
            int t1 = 0,t2=0;
            while(i<n)
            {
                if(A[i]<=R)
                {
                    ++i;
                    t1 += i-j;
                }
                else {
                    ++i;
                    j=i;
                }
            }
            i = 0;
            j = 0;
            while (i < n)
            {
                if (A[i] < L)
                {
                    ++i;
                    t2 += i - j;
                }
                else
                {
                    ++i;
                    j = i;
                }
            }
            ans = t1 - t2;
            return ans;
        
    }
} 
*/
