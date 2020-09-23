using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个无重复元素的有序整数数组，返回数组区间范围的汇总。

示例 1:

输入: [0,1,2,4,5,7]
输出: ["0->2","4->5","7"]
解释: 0,1,2 可组成一个连续的区间; 4,5 可组成一个连续的区间。
示例 2:

输入: [0,2,3,4,6,8,9]
输出: ["0","2->4","6","8->9"]
解释: 2,3,4 可组成一个连续的区间; 8,9 可组成一个连续的区间。

 
*/
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
汇总区间
力扣 (LeetCode)
发布于 2019-06-24
4.9k
思路

对于包含连续元素的一段区间。如果相邻的元素之间的差值大于 11，那么这两个元素肯定不属于一段区间。

算法

为了得出这些区间，我们需要找到一种方法将它们分开。题目所给出的数组是有序的，同时还没有重复元素。在这样的数组里面，两个相邻的元素的差值要么等于 1 要么大于 1。对于那些差值等于 1 的就将它们被放在同一段区间内；否则，就将把它们放在不同的区间。

我们还需要知道的是一段区间的起始坐标，这样就可以把它们放进结果里面了。因此，我们需要保存两个坐标，分别代表一段区间的两个分界点。对于遍历到的每个新元素来说，检查一下它是否可以拓展当前的区间，如果不能，就把当前的元素作为一个新的区间的开始。

不要忘记把最后一段区间也放进结果里面。这个逻辑很容易实现，你可以在循环里通过一个特定的判断条件来加入或者在循环结束后加入。


public class Solution {
    public List<String> summaryRanges(int[] nums) {
        List<String> summary = new ArrayList<>();
        for (int i = 0, j = 0; j < nums.length; ++j) {
            // check if j + 1 extends the range [nums[i], nums[j]]
            if (j + 1 < nums.length && nums[j + 1] == nums[j] + 1)
                continue;
            // put the range [nums[i], nums[j]] into the list
            if (i == j)
                summary.add(nums[i] + "");
            else
                summary.add(nums[i] + "->" + nums[j]);
            i = j + 1;
        }
        return summary;
    }
}

复杂度分析

时间复杂度：O(n)O(n)
每个元素都需要被访问常量次数，要么在相邻元素中被比较，要么就是被放入结果的时候被访问。

空间复杂度：O(1)O(1)
如果不考虑输出的话，我们唯一需要额外开辟空间的就是一段区间的两个坐标。

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
