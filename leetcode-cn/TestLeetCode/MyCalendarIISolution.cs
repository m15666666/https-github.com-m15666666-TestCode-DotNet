using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/my-calendar-ii/
/// 731. 我的日程安排表 II
/// https://www.cnblogs.com/strengthen/p/10518918.html
/// </summary>
class MyCalendarIISolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public MyCalendarIISolution()
    {

    }

    public bool Book(int start, int end)
    {
        foreach( var overlap in overlaps)
            if (overlap.Item1 < end && start < overlap.Item2) return false;

        foreach (var interval in intervals)
            if (interval.Item1 < end && start < interval.Item2)
                overlaps.Add((Math.Max(interval.Item1, start), Math.Min(interval.Item2, end)));

        intervals.Add((start, end));
        return true;
    }

    private List<(int, int)> overlaps = new List<(int, int)>();
    private List<(int, int)> intervals = new List<(int, int)>();
}