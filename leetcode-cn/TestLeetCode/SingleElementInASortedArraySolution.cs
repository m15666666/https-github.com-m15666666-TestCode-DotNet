using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个只包含整数的有序数组，每个元素都会出现两次，唯有一个数只会出现一次，找出这个数。

示例 1:

输入: [1,1,2,3,3,4,4,8,8]
输出: 2
示例 2:

输入: [3,3,7,7,10,11,11]
输出: 10
注意: 您的方案应该在 O(log n)时间复杂度和 O(1)空间复杂度中运行。
*/
/// <summary>
/// https://leetcode-cn.com/problems/single-element-in-a-sorted-array/
/// 540. 有序数组中的单一元素
/// </summary>
class SingleElementInASortedArraySolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int SingleNonDuplicate(int[] nums)
    {
        var len = nums.Length;
        if (len == 1) return nums[0];
        for (int i = 1; i < len; i += 2)
            if (nums[i] != nums[i - 1]) return nums[i-1];
        return nums[len - 1];

        // 利用亦或，也测试通过
        //int ret = 0;
        //foreach (var v in nums)
        //    ret ^= v;
        //return ret;
    }
}
/*
public class Solution {
    public int SingleNonDuplicate(int[] nums) {
        int result = 0;
        for (int i = 0; i < nums.Length; i ++) {
            result = result ^ nums[i];
        }
        return result;
    }
}
*/
