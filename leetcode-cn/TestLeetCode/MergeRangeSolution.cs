using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/merge-intervals/
/// 56.合并区间
/// 出一个区间的集合，请合并所有重叠的区间。
/// </summary>
class MergeRangeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    /**
 * Definition for an interval.
 * public class Interval {
 *     public int start;
 *     public int end;
 *     public Interval() { start = 0; end = 0; }
 *     public Interval(int s, int e) { start = s; end = e; }
 * }
 */
    public IList<Interval> Merge(IList<Interval> intervals)
    {
        if (intervals == null || intervals.Count < 2) return intervals;

        var array = intervals.ToArray();
        Array.Sort(array, new StartComparer());

        List<Interval> ret = new List<Interval>();
        Interval last = null;

        foreach( var item in array)
        {
            if( last != null && ( last.start <= item.start && item.start <= last.end ) )
            {
                last.end = Math.Max(last.end, item.end);
                continue;
            }

            last = item;
            ret.Add(item);
        }

        return ret;
    }

    private class StartComparer : IComparer<Interval>
    {
        public int Compare(Interval x, Interval y)
        {
            return x.start.CompareTo(y.start);
        }
    }
}

/**
 * Definition for an interval.
 * */
public class Interval {
    public int start;
    public int end;
    public Interval() { start = 0; end = 0; }
    public Interval(int s, int e) { start = s; end = e; }
}