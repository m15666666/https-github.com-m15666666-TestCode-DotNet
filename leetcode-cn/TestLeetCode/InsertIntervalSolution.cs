using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给出一个无重叠的 ，按照区间起始端点排序的区间列表。

在列表中插入一个新的区间，你需要确保列表中的区间仍然有序且不重叠（如果有必要的话，可以合并区间）。

示例 1:

输入: intervals = [[1,3],[6,9]], newInterval = [2,5]
输出: [[1,5],[6,9]]
示例 2:

输入: intervals = [[1,2],[3,5],[6,7],[8,10],[12,16]], newInterval = [4,8]
输出: [[1,2],[3,10],[12,16]]
解释: 这是因为新的区间 [4,8] 与 [3,5],[6,7],[8,10] 重叠。
*/
/// <summary>
/// https://leetcode-cn.com/problems/insert-interval/
/// 57. 插入区间
/// 
/// </summary>
class InsertIntervalSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int[][] Insert(int[][] intervals, int[] newInterval) 
    {
        int newStart = newInterval[0], newEnd = newInterval[1];
        int index = 0, n = intervals.Length;
        LinkedList<int[]> output = new LinkedList<int[]>();

        int[] last = null;
        while (index < n && newStart > intervals[index][0])
          output.AddLast(last = intervals[index++]);

        if (0 == output.Count || last[1] < newStart) output.AddLast(newInterval);
        else last[1] = Math.Max(last[1], newEnd);
        
        while (index < n) 
        {
          var interval = intervals[index++];
          int start = interval[0], end = interval[1];
          last = output.Last.Value;
          if (last[1] < start) output.AddLast(interval);
          else last[1] = Math.Max(last[1], end);
        }
        return output.ToArray();
    }
}
/*

插入区间
力扣 (LeetCode)
发布于 4 个月前
5.7k
方法：贪心
贪心算法：
贪心算法一般用来解决需要 “找到要做某事的最小数量” 或 “找到在某些情况下适合的最大物品数量” 的问题，且提供的是无序的输入。

贪心算法的思想是每一步都选择最佳解决方案，最终获得全局最佳的解决方案。

标准解决方案具有 \mathcal{O}(N \log N)O(NlogN) 的时间复杂度且由以下两部分组成：

思考如何排序输入数据（\mathcal{O}(N \log N)O(NlogN) 的时间复杂度）。
思考如何解析排序后的数据（\mathcal{O}(N)O(N) 的时间复杂度）
如果输入数据本身有序，则我们不需要进行排序，那么该贪心算法具有 \mathcal{O}(N)O(N) 的时间复杂度。

如何证明你的贪心思想具有全局最优的效果：可以使用反证法来证明。

让我们来看下面的例子来理解：

在这里插入图片描述

我们可以分为三个步骤去实现它：

在区间 newInterval 之前开始的区间全部添加到输出中。
在这里插入图片描述

将 newInterval 添加到输出中，如果与输出中的最后一个区间重合则合并它们。
在这里插入图片描述

然后一个个添加后续的区间，如果重合则合并它们。
在这里插入图片描述

算法：

将 newInterval 之前开始的区间添加到输出。
添加 newInterval 到输出，若 newInterval 与输出中的最后一个区间重合则合并他们。
一个个添加区间到输出，若有重叠部分则合并他们。
class Solution {
  public int[][] insert(int[][] intervals, int[] newInterval) {
    // init data
    int newStart = newInterval[0], newEnd = newInterval[1];
    int idx = 0, n = intervals.length;
    LinkedList<int[]> output = new LinkedList<int[]>();

    // add all intervals starting before newInterval
    while (idx < n && newStart > intervals[idx][0])
      output.add(intervals[idx++]);

    // add newInterval
    int[] interval = new int[2];
    // if there is no overlap, just add the interval
    if (output.isEmpty() || output.getLast()[1] < newStart)
      output.add(newInterval);
    // if there is an overlap, merge with the last interval
    else {
      interval = output.removeLast();
      interval[1] = Math.max(interval[1], newEnd);
      output.add(interval);
    }

    // add next intervals, merge with newInterval if needed
    while (idx < n) {
      interval = intervals[idx++];
      int start = interval[0], end = interval[1];
      // if there is no overlap, just add an interval
      if (output.getLast()[1] < start) output.add(interval);
      // if there is an overlap, merge with the last interval
      else {
        interval = output.removeLast();
        interval[1] = Math.max(interval[1], end);
        output.add(interval);
      }
    }
    return output.toArray(new int[output.size()][2]);
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N)O(N)。我们只遍历了一次输入元素。
空间复杂度：\mathcal{O}(N)O(N)，输出答案所使用的空间。
下一篇：二分法查找插入的interval的起点和终点相邻区域

public class Solution {
    public int[][] Insert(int[][] intervals, int[] newInterval) {
        List<int[]> result=new List<int[]>();
        int i=0;
        while(i<intervals.Length&&intervals[i][1]<newInterval[0])
        {
            result.Add(intervals[i]);
            i++;
        }
        while(i<intervals.Length&&newInterval[1]>=intervals[i][0])
        {
            newInterval[0]=Math.Min(newInterval[0],intervals[i][0]);
            newInterval[1]=Math.Max(newInterval[1],intervals[i][1]);
            i++;
        }
        result.Add(newInterval);

        while(i<intervals.Length)
        {
            result.Add(intervals[i]);
            i++;
        }
        return result.ToArray();
    }
}
*/
