using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个区间的集合，找到需要移除区间的最小数量，使剩余区间互不重叠。

注意:

可以认为区间的终点总是大于它的起点。
区间 [1,2] 和 [2,3] 的边界相互“接触”，但没有相互重叠。
示例 1:

输入: [ [1,2], [2,3], [3,4], [1,3] ]

输出: 1

解释: 移除 [1,3] 后，剩下的区间没有重叠。
示例 2:

输入: [ [1,2], [1,2], [1,2] ]

输出: 2

解释: 你需要移除两个 [1,2] 来使剩下的区间没有重叠。
示例 3:

输入: [ [1,2], [2,3] ]

输出: 0

解释: 你不需要移除任何区间，因为它们已经是无重叠的了。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/non-overlapping-intervals/
/// 435. 无重叠区间
/// https://blog.csdn.net/qq_36946274/article/details/81591172
/// 
/// </summary>
class NonOverlappingIntervalsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int EraseOverlapIntervals(int[][] intervals)
    {
        if (intervals == null || intervals.Length < 2) return 0;

        const int EndIndex = 1;
        const int StartIndex = 0;
        Comparison<int[]> action = (x, y) => { return x[EndIndex].CompareTo(y[EndIndex]); };
        Array.Sort(intervals, action);

        int length = intervals.Length;
        int count = 1;
        int last = 0;
        for (int i = 1; i < length; i++)
        {
            if (intervals[last][EndIndex] <= intervals[i][StartIndex])
            {
                count++;
                last = i;
            }
        }
        return length - count;
    }
}