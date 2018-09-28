using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/4sum/
/// 四数之和
/// 给定一个包含 n 个整数的数组 nums 和一个目标值 target，
/// 判断 nums 中是否存在四个元素 a，b，c 和 d ，使得 a + b + c + d 的值与 target 相等？
/// 找出所有满足条件且不重复的四元组。
/// </summary>
class FourSumSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public IList<IList<int>> FourSum(int[] nums, int target)
    {
        List<IList<int>> ret = new List<IList<int>>();

        if (nums == null || nums.Length < 4) return ret;

        Array.Sort(nums);

        HashSet<string> matches = new HashSet<string>();

        for (int i = 0; i < nums.Length - 3; ++i)
        {
            var v = nums[i];

            var list = ThreeSum(nums, target - v, i + 1, nums.Length - 1);
            if (list.Count == 0) continue;

            foreach( var sublist in list)
            {
                string key = $"{v}-{sublist[0]}-{sublist[1]}-{sublist[2]}";
                if (matches.Contains(key)) continue;

                matches.Add(key);

                sublist.Insert(0, v);
                ret.Add(sublist);
            }
        }

        return ret;
    }

    private IList<IList<int>> ThreeSum( int[] nums, int target, int startIndex, int endIndex )
    {
        List<IList<int>> ret = new List<IList<int>>();

        for (int i = startIndex; i < endIndex - 1; ++i)
        {
            int l = i + 1, r = nums.Length - 1;
            while (l < r)
            {
                int sum = nums[i] + nums[l] + nums[r];
                var difference = sum - target;

                if (difference == 0) ret.Add( new List<int> { nums[i], nums[l], nums[r] });

                if (difference <= 0) ++l;
                else --r;
            }
        }

        return ret;
    }
}