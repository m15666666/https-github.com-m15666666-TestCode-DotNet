using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个整数数组 nums ，你可以对它进行一些操作。

每次操作中，选择任意一个 nums[i] ，删除它并获得 nums[i] 的点数。之后，你必须删除每个等于 nums[i] - 1 或 nums[i] + 1 的元素。

开始你拥有 0 个点数。返回你能通过这些操作获得的最大点数。

示例 1:

输入: nums = [3, 4, 2]
输出: 6
解释: 
删除 4 来获得 4 个点数，因此 3 也被删除。
之后，删除 2 来获得 2 个点数。总共获得 6 个点数。
示例 2:

输入: nums = [2, 2, 3, 3, 3, 4]
输出: 9
解释: 
删除 3 来获得 3 个点数，接着要删除两个 2 和 4 。
之后，再次删除 3 获得 3 个点数，再次删除 3 获得 3 个点数。
总共获得 9 个点数。
注意:

nums的长度最大为20000。
每个整数nums[i]的大小都在[1, 10000]范围内。
*/
/// <summary>
/// https://leetcode-cn.com/problems/delete-and-earn/
/// 740. 删除与获得点数
/// https://blog.csdn.net/OneDeveloper/article/details/80001202
/// </summary>
class DeleteAndEarnSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int DeleteAndEarn(int[] nums)
    {
        if (nums == null || nums.Length == 0) return 0;
        
        ushort[] numCount = new ushort[10001];
        int max = 0;
        int l = nums.Length;
        for (int i = 0; i < l; i++)
        {
            var v = nums[i];
            numCount[v]++;
            if (max < v) max = v;
        }

        int t1 = 0;
        int t2 = numCount[1];
        for (int i = 2; i <= max; i++)
        {
            //var tI = Math.Max(t2, numCount[i] * i + t1);
            //t1 = t2;
            //t2 = tI;

            (t2,t1) = (Math.Max(t2, numCount[i] * i + t1),t2);
        }

        return t2;
    }
}