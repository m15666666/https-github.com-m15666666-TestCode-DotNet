﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个表示分数的非负整数数组。 玩家1从数组任意一端拿取一个分数，随后玩家2继续从剩余数组任意一端拿取分数，然后玩家1拿，……。
每次一个玩家只能拿取一个分数，分数被拿取之后不再可取。直到没有剩余分数可取时游戏结束。最终获得分数总和最多的玩家获胜。

给定一个表示分数的数组，预测玩家1是否会成为赢家。你可以假设每个玩家的玩法都会使他的分数最大化。

示例 1:

输入: [1, 5, 2]
输出: False
解释: 一开始，玩家1可以从1和2中进行选择。
如果他选择2（或者1），那么玩家2可以从1（或者2）和5中进行选择。如果玩家2选择了5，那么玩家1则只剩下1（或者2）可选。
所以，玩家1的最终分数为 1 + 2 = 3，而玩家2为 5。
因此，玩家1永远不会成为赢家，返回 False。
示例 2:

输入: [1, 5, 233, 7]
输出: True
解释: 玩家1一开始选择1。然后玩家2必须从5和7中进行选择。无论玩家2选择了哪个，玩家1都可以选择233。
最终，玩家1（234分）比玩家2（12分）获得更多的分数，所以返回 True，表示玩家1可以成为赢家。
注意:

1 <= 给定的数组长度 <= 20.
数组里所有分数都为非负数且不会大于10000000。
如果最终两个玩家的分数相等，那么玩家1仍为赢家。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/predict-the-winner/
/// 486. 预测赢家
/// https://blog.csdn.net/OneDeveloper/article/details/79929819
/// </summary>
class PredictTheWinnerSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool PredictTheWinner(int[] nums)
    {
        int n = nums.Length;

        // dp[left,right]表示，left<-->right 区间内 先取玩家 总得分与 后取玩家 总得分的差值，注意：先取玩家和后取玩家总是在变动。
        int[,] dp = new int[n,n];
        dp[n - 1,n - 1] = nums[n - 1];
        for (int left = n - 2; left >= 0; left--)
        {
            for (int right = left; right < n; right++)
            {
                if (left == right)
                {
                    dp[left,right] = nums[left];
                    continue;
                }

                // 首先去左1
                int pickLeft = nums[left] - dp[left + 1,right];

                // 首先取友1
                int pickRight = nums[right] - dp[left,right - 1];

                dp[left,right] = Math.Max(pickLeft, pickRight);
            }
        }
        return dp[0,n - 1] >= 0;
    }
}