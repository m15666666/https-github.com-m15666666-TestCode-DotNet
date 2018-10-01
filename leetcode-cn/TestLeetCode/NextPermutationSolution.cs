using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/next-permutation/
/// 下一个排列
/// 实现获取下一个排列的函数，算法需要将给定数字序列重新排列成字典序中下一个更大的排列。
/// 如果不存在下一个更大的排列，则将数字重新排列成最小的排列（即升序排列）。
/// 必须原地修改，只允许使用额外常数空间。
/// https://leetcode-cn.com/problems/next-permutation/solution/
/// </summary>
class NextPermutationSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public void NextPermutation(int[] nums)
    {
        if (nums == null || nums.Length < 2) return;

        int decreaseIndex = -1;
        for( int i = nums.Length - 1; 0 < i; i--)
        {
            if( nums[i-1] < nums[i])
            {
                decreaseIndex = i - 1;
                break;
            }
        }

        if( decreaseIndex == -1)
        {
            Array.Reverse(nums);
            return;
        }

        var length = nums.Length - 1 - decreaseIndex;
        if( 1 < length ) Array.Sort( nums, decreaseIndex + 1, nums.Length - 1 - decreaseIndex );

        var a = nums[decreaseIndex];
        for( int i = decreaseIndex + 1; i < nums.Length; i++)
        {
            var b = nums[i];
            if( a < b)
            {
                nums[i] = a;
                nums[decreaseIndex] = b;
                return;
            }
        }
    }
}