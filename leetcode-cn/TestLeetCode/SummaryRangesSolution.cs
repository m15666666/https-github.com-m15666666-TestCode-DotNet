using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/summary-ranges/
/// 228. 汇总区间
/// 给定一个无重复元素的有序整数数组，返回数组区间范围的汇总。
/// 示例 1:
/// 输入: [0,1,2,4,5,7]
/// 输出: ["0->2","4->5","7"]
/// 解释: 0,1,2 可组成一个连续的区间; 4,5 可组成一个连续的区间。
/// 示例 2:
/// 输入: [0,2,3,4,6,8,9]
/// 输出: ["0","2->4","6","8->9"]
/// 解释: 2,3,4 可组成一个连续的区间; 8,9 可组成一个连续的区间。
/// </summary>
class SummaryRangesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    private static readonly StringBuilder sb = new StringBuilder();
    public IList<string> SummaryRanges(int[] nums)
    {
        if (nums == null || nums.Length == 0) return new string[0];
        if (nums.Length == 1) return new string[] { nums[0].ToString() };

        List<string> ret = new List<string>(nums.Length);
        sb.Clear();
        int prev = 0;
        bool hasRange = false;
        foreach( var v in nums)
        {
            if( sb.Length == 0)
            {
                prev = v;
                sb.Append(prev.ToString());
                continue;
            }
            if( 1 + prev == v )
            {
                ++prev;
                hasRange = true;
                continue;
            }

            if (hasRange)
            {
                sb.Append("->");
                sb.Append(prev.ToString());
            }
            ret.Add(sb.ToString());
            sb.Clear();
            prev = v;
            sb.Append(prev.ToString());
            hasRange = false;
        }
        if (hasRange)
        {
            sb.Append("->");
            sb.Append(prev.ToString());
        }
        ret.Add(sb.ToString());

        return ret;
    }

    //public IList<string> SummaryRanges(int[] nums)
    //{
    //    if (nums == null || nums.Length == 0) return new string[0];

    //    List<string> ret = new List<string>(nums.Length);
    //    int start = nums[0];
    //    int stop = start;
    //    for( int index = 1; index < nums.Length; index++)
    //    {
    //        var v = nums[index];
    //        if( 1 + stop == v )
    //        {
    //            ++stop;
    //            continue;
    //        }

    //        if (start == stop) ret.Add(start.ToString());
    //        else ret.Add( string.Format("{0}->{1}", start, stop) ); // $"{start}->{stop}"
    //        stop = start = v;
    //    }
    //    if(start == stop) ret.Add(start.ToString());
    //    else ret.Add(string.Format("{0}->{1}", start, stop)); // $"{start}->{stop}"

    //    return ret;
    //}
}
/*
//别人的算法
public class Solution {
    public IList<string> SummaryRanges(int[] nums) {
        List<string> summaryList = new List<string>();
        int firstInd = 0;
        int lastInd = 0;
        string str = "";
        for (int i = 0; i < nums.Length; ++i)
        {
            if (i == 0)
            {
                str = nums[firstInd] + "";
                lastInd++;
            }
            else if (nums[i] == nums[i - 1] + 1)
            {
                str = nums[firstInd] + "->" + nums[lastInd];
                lastInd++;
            }
            else
            {
                summaryList.Add(str);
                firstInd = i;
                str = nums[firstInd] + "";
                lastInd = i + 1;
            }
            if (i == nums.Length - 1)
            {
                summaryList.Add(str);
            }
        }
        return summaryList;
    }
}
*/
