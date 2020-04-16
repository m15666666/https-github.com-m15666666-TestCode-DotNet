using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个非负整数数组，你最初位于数组的第一个位置。

数组中的每个元素代表你在该位置可以跳跃的最大长度。

你的目标是使用最少的跳跃次数到达数组的最后一个位置。

示例:

输入: [2,3,1,1,4]
输出: 2
解释: 跳到最后一个位置的最小跳跃数是 2。
     从下标为 0 跳到下标为 1 的位置，跳 1 步，然后跳 3 步到达数组的最后一个位置。
说明:

假设你总是可以到达数组的最后一个位置。
*/
/// <summary>
/// https://leetcode-cn.com/problems/jump-game-ii/ 
/// 45. 跳跃游戏 II
/// 
/// 
/// </summary>
class JumpGameIISolution
{
    public void Test()
    {
        int[] nums = new int[] {2,3,0,1,4};
        var ret = Jump(nums);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int Jump(int[] nums) 
    {
        if (nums == null || nums.Length < 2) return 0;

        int ret = 0;
        int start = 0;
        int end = 1;
        int len = nums.Length;
        do
        {
            int maxPos = 0;
            for (int i = start; i < end; i++)
                maxPos = Math.Max(maxPos, i + nums[i]);
            start = end;
            end = maxPos + 1;
            ret++;
        } while (end < len);
        return ret;
    }
    //public int Jump(int[] nums) {
    //    int len = nums.Length;
    //    int[] dp = new int[len];
    //    dp[len - 1] = 0;
    //    for(int i = len - 2; -1 < i; i--)
    //    {
    //        var v = nums[i];
    //        if( v < 1)
    //        {
    //            dp[i] = len;
    //            continue;
    //        }

    //        if (len - 1 <= i + v)
    //        {
    //            dp[i] = 1;
    //            continue;
    //        }

    //        int min = dp[i+1];
    //        for( int j = i + 2; j <= i + v; j++)
    //        {
    //            if (dp[j] < min) min = dp[j];
    //        }

    //        dp[i] = min + 1;
    //    }
    //    return dp[0];
    //}
}
/*

https://leetcode-cn.com/problems/jump-game-ii/solution/45-by-ikaruga/
【跳跃游戏 II】别想那么多，就挨着跳吧 II
Ikaruga
发布于 6 个月前
9.3k
思路
如果某一个作为 起跳点 的格子可以跳跃的距离是 3，那么表示后面 3 个格子都可以作为 起跳点。
11. 可以对每一个能作为 起跳点 的格子都尝试跳一次，把 能跳到最远的距离 不断更新。

如果从这个 起跳点 起跳叫做第 1 次 跳跃，那么从后面 3 个格子起跳 都 可以叫做第 2 次 跳跃。

所以，当一次 跳跃 结束时，从下一个格子开始，到现在 能跳到最远的距离，都 是下一次 跳跃 的 起跳点。
31. 对每一次 跳跃 用 for 循环来模拟。
32. 跳完一次之后，更新下一次 起跳点 的范围。
33. 在新的范围内跳，更新 能跳到最远的距离。

记录 跳跃 次数，如果跳到了终点，就得到了结果。

图解
图片.png

代码
int jump(vector<int> &nums)
{
    int ans = 0;
    int start = 0;
    int end = 1;
    while (end < nums.size())
    {
        int maxPos = 0;
        for (int i = start; i < end; i++)
        {
            // 能跳到最远的距离
            maxPos = max(maxPos, i + nums[i]);
        }
        start = end;      // 下一次起跳点范围开始的格子
        end = maxPos + 1; // 下一次起跳点范围结束的格子
        ans++;            // 跳跃次数
    }
    return ans;
}
优化
从上面代码观察发现，其实被 while 包含的 for 循环中，i 是从头跑到尾的。

只需要在一次 跳跃 完成时，更新下一次 能跳到最远的距离。

并以此刻作为时机来更新 跳跃 次数。

就可以在一次 for 循环中处理。

int jump(vector<int>& nums)
{
    int ans = 0;
    int end = 0;
    int maxPos = 0;
    for (int i = 0; i < nums.size() - 1; i++)
    {
        maxPos = max(nums[i] + i, maxPos);
        if (i == end)
        {
            end = maxPos;
            ans++;
        }
    }
    return ans;
}

public class Solution {
     public int Jump(int[] nums)
        {
            if (nums.Length == 1) 
            {
                return 0;
            }
            int current = 0;
            int step = 0;
            while (current+nums[current] < nums.Length-1) 
            {
                int max = 0;
                int jump = 0;
                for (int i = 1; i <= nums[current]; i++)
                {
                    if (nums[current + i] + i > max) 
                    {
                        max = nums[current + i] + i;
                        jump = i;
                    }
                    
                }
                current += jump;
                step++;
            }
            step++;
            return step;
        }
} 

public class Solution {
     public int Jump(int[] nums)
        {
            if (nums.Length == 1) 
            {
                return 0;
            }
            int current = 0;
            int step = 0;
            while (current+nums[current] < nums.Length-1) 
            {
                int max = 0;
                int jump = 0;
                for (int i = 1; i <= nums[current]; i++)
                {
                    if (nums[current + i] + i > max) 
                    {
                        max = nums[current + i] + i;
                        jump = i;
                    }
                    
                }
                current += jump;
                step++;
            }
            step++;
            return step;
        }
}
     
*/
