using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个非负整数数组，你最初位于数组的第一个位置。

数组中的每个元素代表你在该位置可以跳跃的最大长度。

判断你是否能够到达最后一个位置。

示例 1:

输入: [2,3,1,1,4]
输出: true
解释: 我们可以先跳 1 步，从位置 0 到达 位置 1, 然后再从位置 1 跳 3 步到达最后一个位置。
示例 2:

输入: [3,2,1,0,4]
输出: false
解释: 无论怎样，你总会到达索引为 3 的位置。但该位置的最大跳跃长度是 0 ， 所以你永远不可能到达最后一个位置。
*/
/// <summary>
/// https://leetcode-cn.com/problems/jump-game/
/// 55. 跳跃游戏
/// 
/// </summary>
class CanJumpSolution
{
    public static void Test()
    {
        int[] nums = new int[] { 2, 0, 6, 9, 8, 4, 5, 0, 8, 9, 1, 2, 9, 6, 8, 8, 0, 6, 3, 1, 2, 2, 1, 2, 6, 5, 3, 1, 2, 2, 6, 4, 2, 4, 3, 0, 0, 0, 3, 8, 2, 4, 0, 1, 2, 0, 1, 4, 6, 5, 8, 0, 7, 9, 3, 4, 6, 6, 5, 8, 9, 3, 4, 3, 7, 0, 4, 9, 0, 9, 8, 4, 3, 0, 7, 7, 1, 9, 1, 9, 4, 9, 0, 1, 9, 5, 7, 7, 1, 5, 8, 2, 8, 2, 6, 8, 2, 2, 7, 5, 1, 7, 9, 6 };
        var a = CanJump(nums);
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public static bool CanJump(int[] nums)
    {
        if(nums == null || nums.Length == 1) return true;

        for (int i = 0, rightmost = 0, n = nums.Length; i < n && i <= rightmost; ++i) 
        {
            int right = i + nums[i];
            if (rightmost < right)
            {
                rightmost = right;
                if (n - 1 <= rightmost) return true;
            }
        }
        return false;
    }

    //public static bool CanJump(int[] nums)
    //{
    //    if (nums == null || nums.Length < 2) return true;
    //    Dictionary<int, bool> index2Canjump = new Dictionary<int, bool>();
    //    return CanJump0(nums, nums.Length - 1, index2Canjump );
    //}

    //private static bool CanJump0( int[] nums, int targetIndex, Dictionary<int, bool> index2Canjump )
    //{
    //    if (index2Canjump.ContainsKey(targetIndex)) return index2Canjump[targetIndex];

    //    Console.WriteLine($"targetIndex:{targetIndex}");

    //    if (targetIndex == 0) return true;
    //    bool ret = false;
    //    for ( int i = targetIndex - 1; -1 < i; i-- )
    //    {
    //        var v = nums[i];
    //        if (targetIndex - i <= v)
    //        {
    //            var canJump = CanJump0(nums, i, index2Canjump );
    //            if (canJump)
    //            {
    //                ret = true;
    //                break;
    //            }
    //        }
    //    }

    //    index2Canjump[targetIndex] = ret;
    //    return ret;
    //}
}
/*

跳跃游戏
力扣官方题解
发布于 10 天前
15.0k
方法一：贪心
我们可以用贪心的方法解决这个问题。

设想一下，对于数组中的任意一个位置 yy，我们如何判断它是否可以到达？根据题目的描述，只要存在一个位置 xx，它本身可以到达，并且它跳跃的最大长度为 x + \textit{nums}[x]x+nums[x]，这个值大于等于 yy，即 x + \textit{nums}[x] \geq yx+nums[x]≥y，那么位置 yy 也可以到达。

换句话说，对于每一个可以到达的位置 xx，它使得 x+1, x+2, \cdots, x+\textit{nums}[x]x+1,x+2,⋯,x+nums[x] 这些连续的位置都可以到达。

这样以来，我们依次遍历数组中的每一个位置，并实时维护 最远可以到达的位置。对于当前遍历到的位置 xx，如果它在 最远可以到达的位置 的范围内，那么我们就可以从起点通过若干次跳跃到达该位置，因此我们可以用 x + \textit{nums}[x]x+nums[x] 更新 最远可以到达的位置。

在遍历的过程中，如果 最远可以到达的位置 大于等于数组中的最后一个位置，那就说明最后一个位置可达，我们就可以直接返回 True 作为答案。反之，如果在遍历结束后，最后一个位置仍然不可达，我们就返回 False 作为答案。

以题目中的示例一

[2, 3, 1, 1, 4]
为例：

我们一开始在位置 00，可以跳跃的最大长度为 22，因此最远可以到达的位置被更新为 22；

我们遍历到位置 11，由于 1 \leq 21≤2，因此位置 11 可达。我们用 11 加上它可以跳跃的最大长度 33，将最远可以到达的位置更新为 44。由于 44 大于等于最后一个位置 44，因此我们直接返回 True。

我们再来看看题目中的示例二

[3, 2, 1, 0, 4]
我们一开始在位置 00，可以跳跃的最大长度为 33，因此最远可以到达的位置被更新为 33；

我们遍历到位置 11，由于 1 \leq 31≤3，因此位置 11 可达，加上它可以跳跃的最大长度 22 得到 33，没有超过最远可以到达的位置；

位置 22、位置 33 同理，最远可以到达的位置不会被更新；

我们遍历到位置 44，由于 4 > 34>3，因此位置 44 不可达，我们也就不考虑它可以跳跃的最大长度了。

在遍历完成之后，位置 44 仍然不可达，因此我们返回 False。

public class Solution {
    public boolean canJump(int[] nums) {
        int n = nums.length;
        int rightmost = 0;
        for (int i = 0; i < n; ++i) {
            if (i <= rightmost) {
                rightmost = Math.max(rightmost, i + nums[i]);
                if (rightmost >= n - 1) {
                    return true;
                }
            }
        }
        return false;
    }
}
复杂度分析

时间复杂度：O(n)O(n)，其中 nn 为数组的大小。只需要访问 nums 数组一遍，共 nn 个位置。

空间复杂度：O(1)O(1)，不需要额外的空间开销。

下一篇：【跳跃游戏】别想那么多，就挨着跳吧

public class Solution {
    public bool CanJump(int[] nums) {
        if(nums == null || nums.Length < 2)
        {
            return true;
        }
        int n = nums.Length;
        int cur = -1;
        for (int i = n - 2; i >= 0; i--)
        {
            if (cur < 0 && nums[i] == 0)
            {
                cur = i;
            }
            else if (nums[i] > cur - i)
            {
                cur = -1;
            }
        }

        return cur == -1;
    }
}
*/