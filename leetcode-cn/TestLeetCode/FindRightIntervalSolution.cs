using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一组区间，对于每一个区间 i，检查是否存在一个区间 j，它的起始点大于或等于区间 i 的终点，这可以称为 j 在 i 的“右侧”。

对于任何区间，你需要存储的满足条件的区间 j 的最小索引，这意味着区间 j 有最小的起始点可以使其成为“右侧”区间。如果区间 j 不存在，则将区间 i 存储为 -1。最后，你需要输出一个值为存储的区间值的数组。

注意:

你可以假设区间的终点总是大于它的起始点。
你可以假定这些区间都不具有相同的起始点。
示例 1:

输入: [ [1,2] ]
输出: [-1]

解释:集合中只有一个区间，所以输出-1。
示例 2:

输入: [ [3,4], [2,3], [1,2] ]
输出: [-1, 0, 1]

解释:对于[3,4]，没有满足条件的“右侧”区间。
对于[2,3]，区间[3,4]具有最小的“右”起点;
对于[1,2]，区间[2,3]具有最小的“右”起点。
示例 3:

输入: [ [1,4], [2,3], [3,4] ]
输出: [-1, 2, -1]

解释:对于区间[1,4]和[3,4]，没有满足条件的“右侧”区间。
对于[2,3]，区间[3,4]有最小的“右”起点。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/find-right-interval/
/// 436. 寻找右区间
/// https://blog.csdn.net/lv1224/article/details/81751189
/// </summary>
class FindRightIntervalSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[] FindRightInterval(int[][] intervals)
    {
        if (intervals == null || intervals.Length == 0) return new int[0];

        int n = intervals.Length;
        int[] starts = new int[n];
        int[] ret = new int[n];

        Array.Fill(starts, 0);
        Array.Fill(ret, -1);

        Dictionary<int, int> start2Index = new Dictionary<int, int>();
        for( int i = 0; i < n; i++)
        {
            var start = intervals[i][0];
            starts[i] = start;
            start2Index[start] = i;
        }

        Array.Sort(starts);
        for( int i = 0;i < n; i++)
        {
            var end = intervals[i][1];
            ret[i] = Find(start2Index, starts, end);
        }
        return ret;
    }

    private int Find(Dictionary<int, int> start2Index, int[] starts, int end)
    {
        int left = 0;
        int right = starts.Length - 1;

        if (end < starts[left] || starts[right] < end) return -1;

        while( left < right)
        {
            var mid = (left + right) / 2;
            var v = starts[mid];
            if (end <= v) right = mid;
            else left = mid + 1;
        }
        return start2Index[starts[right]];
    }


}
/*
public class Solution {
     public int[] FindRightInterval(int[][] intervals)
     {
         var n = intervals.Length;
         var ans = new int[n];
         var starts = intervals.Select((x, i) => (value: x[0], index: i))
                 .OrderBy(x => x.value).ToList();
         var ends = intervals.Select((x, i) => (value: x[1], index: i))
                 .OrderBy(x => x.value).ToList();

         for(int i = 0, j = 0; i < n; ++i)
         {
             while (j < n && starts[j].value < ends[i].value)
                 ++j;

             if (j == n)
                 ans[ends[i].index] = -1;
             else
                 ans[ends[i].index] = starts[j].index;
         }

         return ans;
     }
} 
*/
