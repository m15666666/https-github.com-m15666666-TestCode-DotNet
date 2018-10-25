using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/sort-colors/
/// 75.颜色分类
/// 给定一个包含红色、白色和蓝色，一共 n 个元素的数组，原地对它们进行排序，
/// 使得相同颜色的元素相邻，并按照红色、白色、蓝色顺序排列。
/// 此题中，我们使用整数 0、 1 和 2 分别表示红色、白色和蓝色。
/// </summary>
class SortColorsSolution
{
    public static void Test()
    {
        int[] nums = new int[] {2, 0, 1};
        SortColors(nums);
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public static void SortColors(int[] nums)
    {
        if (nums == null || nums.Length < 2) return;

        int leftWriteIndex = -1, 
            rightWriteIndex = nums.Length, 
            leftReadIndex = 0,
            rightReadIndex = nums.Length - 1,
            length = nums.Length;

        int whiteColorCount = 0;
        int writeCount = 0;
        var v = nums[leftReadIndex++];
        do
        {
            if (v == 0)
            {
                nums[++leftWriteIndex] = 0;
                writeCount++;
                if (writeCount == length) break;
                v = nums[leftReadIndex++];
            }
            else if (v == 1)
            {
                whiteColorCount++;
                writeCount++;
                if (writeCount == length) break;
                v = nums[leftReadIndex++];
            }
            else if (v == 2)
            {
                writeCount++;
                if (writeCount < length)
                {
                    v = nums[rightReadIndex--];
                    nums[--rightWriteIndex] = 2;
                }
                else if (writeCount == length)
                {
                    nums[--rightWriteIndex] = 2;
                    break;
                }
            }
            else break;
        } while (true);

        if (0 < whiteColorCount)
        {
            int startIndex = leftWriteIndex + 1;
            int endIndex = startIndex + whiteColorCount - 1;
            for (int i = startIndex; i <= endIndex; i++) nums[i] = 1;
        }
    }
}