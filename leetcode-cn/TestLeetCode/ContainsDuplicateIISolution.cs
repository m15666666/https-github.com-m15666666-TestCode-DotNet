using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/contains-duplicate-ii/
/// 219. 存在重复元素 II
/// 给定一个整数数组和一个整数 k，判断数组中是否存在两个不同的索引 i 和 j，
/// 使得 nums [i] = nums [j]，并且 i 和 j 的差的绝对值最大为 k。
/// 示例 1:
/// 输入: nums = [1,2,3,1], k = 3
/// 输出: true
/// 示例 2:
/// 输入: nums = [1,0,1,1], k = 1
/// 输出: true
/// 示例 3:
/// 输入: nums = [1,2,3,1,2,3], k = 2
/// 输出: false
/// </summary>
class ContainsDuplicateIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        if (nums == null || nums.Length < 2 || k < 1 ) return false;

        Dictionary<int, int> num2Index = new Dictionary<int, int>();
        int index = -1;
        foreach( var num in nums)
        {
            index++;
            if (!num2Index.ContainsKey(num))
            {
                num2Index.Add(num, index);
                continue;
            }
            if (index - num2Index[num] <= k) return true;
            num2Index[num] = index;
        }
        return false;
    }
}