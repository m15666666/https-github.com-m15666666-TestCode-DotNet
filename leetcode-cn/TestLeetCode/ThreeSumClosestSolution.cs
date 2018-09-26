using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/3sum-closest/
/// 最接近的三数之和
/// 给定一个包括 n 个整数的数组 nums 和 一个目标值 target。找出 nums 中的三个整数，
/// 使得它们的和与 target 最接近。返回这三个数的和。假定每组输入只存在唯一答案。
/// </summary>
class ThreeSumClosestSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int ThreeSumClosest(int[] nums, int target)
    {
        if (nums == null || nums.Length < 3) return 0;

        Array.Sort(nums);

        int? lastClosest = null;
        for (int i = 0; i < nums.Length; ++i)
        {
            int l = i + 1, r = nums.Length - 1;
            while (l < r)
            {
                int sum = nums[i] + nums[l] + nums[r];
                var difference = sum - target;
                if ( lastClosest == null || Math.Abs(sum - target) < Math.Abs(lastClosest.Value - target) )
                {
                    lastClosest = sum;
                }

                if (difference == 0) return sum;

                if (difference < 0) ++l;
                else --r;
            }
        }

        return lastClosest.Value;
    }
}