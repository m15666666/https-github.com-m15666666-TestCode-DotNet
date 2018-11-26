using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/single-number-ii/
/// 137.只出现一次的数字II
/// 给定一个非空整数数组，除了某个元素只出现一次以外，其余每个元素均出现了三次。找出那个只出现了一次的元素。
/// 说明：
/// 你的算法应该具有线性时间复杂度。 你可以不使用额外空间来实现吗？
/// </summary>
class SingleNumberIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int SingleNumber(int[] nums)
    {
        if (nums == null || nums.Length == 0 || (nums.Length % 3 ) != 1 ) return -1;

        Array.Sort(nums);

        var length = nums.Length;
        int index = 0;

        while( index + 2 < length )
        {
            if (nums[index] != nums[index + 1]) return nums[index];
            index += 3;
        }

        return nums[index];
    }

}