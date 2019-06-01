﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个非空整数数组，找到使所有数组元素相等所需的最小移动数，其中每次移动可将选定的一个元素加1或减1。 您可以假设数组的长度最多为10000。

例如:

输入:
[1,2,3]

输出:
2

说明：
只有两个动作是必要的（记得每一步仅可使其中一个元素加1或减1）： 

[1,2,3]  =>  [2,2,3]  =>  [2,2,2] 
*/
/// <summary>
/// https://leetcode-cn.com/problems/minimum-moves-to-equal-array-elements-ii/
/// 462. 最少移动次数使数组元素相等 II
/// https://www.cnblogs.com/wemo/wemo/p/10480299.html
/// </summary>
class MinimumMovesToEqualArrayElementsIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MinMoves2(int[] nums)
    {
        if (nums == null || nums.Length < 2) return 0;

        Array.Sort(nums);

        int mid = nums[nums.Length / 2];

        int ret = 0;
        foreach (var v in nums) ret += Math.Abs( v - mid);

        return ret;
    }
}
/*
public class Solution {
    public int MinMoves2(int[] nums) {
        int n = nums.Length;
        if (n <= 1) {
            return 0;
        }
        
        Array.Sort(nums);
        int mid = nums[n/2];
        int count = 0;
        for (int i = 0; i < n; i ++) {
            count += Math.Abs(nums[i] - mid);
        }
        
        return count;
    }
} 
*/
