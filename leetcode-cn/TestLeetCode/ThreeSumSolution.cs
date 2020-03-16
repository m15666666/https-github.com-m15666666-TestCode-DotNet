using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你一个包含 n 个整数的数组 nums，判断 nums 中是否存在三个元素 a，b，c ，
使得 a + b + c = 0 ？请你找出所有满足条件且不重复的三元组。

注意：答案中不可以包含重复的三元组。

 

示例：

给定数组 nums = [-1, 0, 1, 2, -1, -4]，

满足要求的三元组集合为：
[
  [-1, 0, 1],
  [-1, -1, 2]
]
*/
/// <summary>
/// https://leetcode-cn.com/problems/3sum/
/// 15. 三数之和
/// 
/// https://www.jianshu.com/p/19b0261c73b9
/// </summary>
class ThreeSumSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<int>> ThreeSum(int[] nums)
    {
        List<IList<int>> ret = new List<IList<int>>();

        if (nums == null || nums.Length < 3) return ret;

        Array.Sort(nums);

        //int n1, n2, n3;
        //n1 = n2 = n3 = -1;
        int upper = nums.Length - 2;
        for (int i = 0; i < upper; ++i)
        {
            var v = nums[i];
            if (0 < v) break;

            // 跳过
            if (0 < i && nums[i - 1] == v) continue;

            int left = i + 1, right = nums.Length - 1;
            while (left < right)
            {
                int sum = v + nums[left] + nums[right];
                if (sum == 0
                    //&& !(n1 == v && n2 == nums[left] && n3 == nums[right])
                    )
                {
                    //if (!(n1 == v && n2 == nums[left] && n3 == nums[right]))
                    //{
                    //    n1 = v;
                    //    n2 = nums[left];
                    //    n3 = nums[right];

                    //    ret.Add(new int[] { n1, n2, n3 });
                    //}

                    ret.Add(new int[] { v, nums[left], nums[right] });

                    while (left < right && nums[left]== nums[left + 1]) ++left;
                    while (left < right && nums[right] == nums[right - 1]) --right;

                    ++left;
                    --right;
                }
                else if (sum < 0) ++left;
                else --right;
            }
        }

        return ret;
    }
}