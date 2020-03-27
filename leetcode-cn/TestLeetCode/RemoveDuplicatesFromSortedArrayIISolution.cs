using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/remove-duplicates-from-sorted-array-ii/
/// 80.删除排序数组中的重复项II
/// 
/// 给定一个排序数组，你需要在原地删除重复出现的元素，使得每个元素最多出现两次，返回移除后数组的新长度。
/// 不要使用额外的数组空间，你必须在原地修改输入数组并在使用 O(1) 额外空间的条件下完成。
/// </summary>
class RemoveDuplicatesFromSortedArrayIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public int RemoveDuplicates(int[] nums)
    {
        if (nums == null) return 0;
        if (nums.Length < 3) return nums.Length;

        int last = nums[0];
        int lastCount = 1;
        int writeIndex = 1;
        int readIndex = 1;
        for(; readIndex < nums.Length; readIndex++)
        {
            var v = nums[readIndex];
            if (v == last)
            {
                lastCount++;
            }
            else
            {
                last = v;
                lastCount = 1;
            }

            // 写入
            if( lastCount < 3 )
            {
                if (writeIndex != readIndex) nums[writeIndex] = v;
                writeIndex++;
            }
        }
        return writeIndex;
    }
}