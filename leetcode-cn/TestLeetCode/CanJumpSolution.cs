using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/jump-game/
/// 55. 跳跃游戏
/// 给定一个非负整数数组，你最初位于数组的第一个位置。
/// 数组中的每个元素代表你在该位置可以跳跃的最大长度。
/// 判断你是否能够到达最后一个位置。
/// </summary>
class CanJumpSolution
{
    public static void Test()
    {
        int[] nums = new int[] { 2, 0, 6, 9, 8, 4, 5, 0, 8, 9, 1, 2, 9, 6, 8, 8, 0, 6, 3, 1, 2, 2, 1, 2, 6, 5, 3, 1, 2, 2, 6, 4, 2, 4, 3, 0, 0, 0, 3, 8, 2, 4, 0, 1, 2, 0, 1, 4, 6, 5, 8, 0, 7, 9, 3, 4, 6, 6, 5, 8, 9, 3, 4, 3, 7, 0, 4, 9, 0, 9, 8, 4, 3, 0, 7, 7, 1, 9, 1, 9, 4, 9, 0, 1, 9, 5, 7, 7, 1, 5, 8, 2, 8, 2, 6, 8, 2, 2, 7, 5, 1, 7, 9, 6 };
        var a =CanJump(nums);
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public static bool CanJump(int[] nums)
    {
        if (nums == null || nums.Length < 2) return true;
        Dictionary<int, bool> index2Canjump = new Dictionary<int, bool>();
        return CanJump0(nums, nums.Length - 1, index2Canjump );
    }

    private static bool CanJump0( int[] nums, int targetIndex, Dictionary<int, bool> index2Canjump )
    {
        if (index2Canjump.ContainsKey(targetIndex)) return index2Canjump[targetIndex];

        Console.WriteLine($"targetIndex:{targetIndex}");

        if (targetIndex == 0) return true;
        bool ret = false;
        for ( int i = targetIndex - 1; -1 < i; i-- )
        {
            var v = nums[i];
            if (targetIndex - i <= v)
            {
                var canJump = CanJump0(nums, i, index2Canjump );
                if (canJump)
                {
                    ret = true;
                    break;
                }
            }
        }

        index2Canjump[targetIndex] = ret;
        return ret;
    }

    //private bool CanJump0( int[] nums, int currentIndex, int targetIndex)
}