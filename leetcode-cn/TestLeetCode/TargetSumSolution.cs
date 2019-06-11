using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个非负整数数组，a1, a2, ..., an, 和一个目标数，S。现在你有两个符号 + 和 -。对于数组中的任意一个整数，你都可以从 + 或 -中选择一个符号添加在前面。

返回可以使最终数组和为目标数 S 的所有添加符号的方法数。

示例 1:

输入: nums: [1, 1, 1, 1, 1], S: 3
输出: 5
解释: 

-1+1+1+1+1 = 3
+1-1+1+1+1 = 3
+1+1-1+1+1 = 3
+1+1+1-1+1 = 3
+1+1+1+1-1 = 3

一共有5种方法让最终目标和为3。
注意:

数组的长度不会超过20，并且数组中的值全为正数。
初始的数组的和不会超过1000。
保证返回的最终结果为32位整数。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/target-sum/
/// 494. 目标和
/// </summary>
class TargetSumSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int FindTargetSumWays(int[] nums, int S)
    {
        _count = 0;
        FindTargetSumWays(0, nums, S);
        return _count;
    }

    private int _count = 0;
    private void FindTargetSumWays(int startIndex, int[] nums, int S)
    {
        var v = nums[startIndex];
        if (startIndex == nums.Length - 1)
        {
            if (S == v) _count++;
            if (S == -v) _count++;

            return;
        }

        FindTargetSumWays(startIndex + 1, nums, S - v);
        FindTargetSumWays(startIndex + 1, nums, S + v);
    }
}
/*
public class Solution {
    public int FindTargetSumWays(int[] nums, int S)
    {
        //所有数据之和
        int sum = 0;
        var count = nums.Length;
        for (int i = 0; i < count; i++)
        {
            sum += nums[i];
        }


        if (sum < S || (sum + S) % 2 != 0)
            return 0;

        var target = (S + sum) / 2;
        var arr = new int[target + 1];
        arr[0] = 1;

        for (int i = 0; i < count; i++)
        {
            for (int j = target; j >= nums[i]; j--)
            {
                arr[j] += arr[j - nums[i]];
            }
        }

        return arr[target];
    }
}
public class Solution {
    public int FindTargetSumWays(int[] nums, int S)
    {
        return FindFunc(nums, S, 0, nums.Length);
    }
    public int FindFunc(int[] nums, int S, int Head, int End)
    {
        if (Head == End)
            return S == 0 ? 1 : 0;
        int t = nums[Head++];
        return FindFunc(nums, S + t, Head, End) + FindFunc(nums, S - t, Head, End);
    }
}
public class Solution {
    public int FindTargetSumWays(int[] nums, int S)
    {
        var count = 0;
        var sum = 0;
        DFS(nums, S, ref count, 0, sum);
        return count;
    }
    void DFS(int[] nums, int s, ref int count, int counter, int sum)
    {
        if(counter == nums.Length)
        {
            if (sum == s)
                count++;
            return;
        }
        DFS(nums, s, ref count, counter + 1, sum + nums[counter]);
        DFS(nums, s, ref count, counter + 1, sum - nums[counter]);
    }
}
*/
