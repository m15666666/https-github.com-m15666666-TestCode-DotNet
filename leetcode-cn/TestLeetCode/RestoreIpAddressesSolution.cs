using System;
using System.Collections.Generic;
using System.Text;

/*
给定一个只包含数字的字符串，复原它并返回所有可能的 IP 地址格式。

有效的 IP 地址正好由四个整数（每个整数位于 0 到 255 之间组成），整数之间用 '.' 分隔。

 

示例:

输入: "25525511135"
输出: ["255.255.11.135", "255.255.111.35"]

*/

/// <summary>
/// https://leetcode-cn.com/problems/restore-ip-addresses/
/// 93.复原IP地址
/// 给定一个只包含数字的字符串，复原它并返回所有可能的 IP 地址格式。
/// https://blog.csdn.net/u014253011/article/details/80003075
/// </summary>
internal class RestoreIpAddressesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<string> RestoreIpAddresses(string s)
    {
        List<string> ret = new List<string>();
        if (string.IsNullOrWhiteSpace(s) || s.Length < 4) return ret;

        List<(int, int)> segments = new List<(int, int)>();
        StringBuilder builder = new StringBuilder(16);
        int n = s.Length;

        BackTrack(0, 3);

        return ret;

        void BackTrack(int start, int dots)
        {
            int maxCount = Math.Min(n - start - 1, 3);
            for (int count = 1; count <= maxCount; count++)
            {
                if (Valid(start, count))
                {
                    segments.Add((start, count));

                    if (dots == 1) AddToOutput(start + count);
                    else BackTrack(start + count, dots - 1);

                    segments.RemoveAt(segments.Count - 1);
                }
            }
        }

        void AddToOutput(int start)
        {
            int count = n - start;
            if (Valid(start, count))
            {
                foreach (var segment in segments)
                    builder.Append(s.Substring(segment.Item1, segment.Item2)).Append('.');

                builder.Append(s.Substring(start));
                ret.Add(builder.ToString());
                builder.Clear();
            }
        }

        bool Valid(int startIndex, int count)
        {
            if (3 < count) return false;

            var firstChar = s[startIndex];
            if (1 < count && firstChar == '0') return false;
            if (3 == count)
            {
                if ('2' < firstChar) return false;

                var secondChar = s[startIndex + 1];
                var thirdChar = s[startIndex + 2];

                if (firstChar == '2' && ('5' < secondChar || ('5' == secondChar && '5' < thirdChar))) return false;
            }

            return true;
        }
    }

    //public IList<string> RestoreIpAddresses(string s)
    //{
    //    List<string> ret = new List<string>();
    //    if (string.IsNullOrWhiteSpace(s) || s.Length < 4 ) return ret;

    //    int slotCount = s.Length - 1;

    //    List<int> list = new List<int>(4) { 0 };

    //    BackTrack(s, slotCount, 1, list, ret);

    //    return ret;
    //}

    //private static void BackTrack(string s, int slotCount, int startIndex, List<int> list, List<string> ret )
    //{
    //    var strStartIndex = list[list.Count - 1];
    //    var sLength = s.Length;
    //    var needPartCount = 4 - ( list.Count - 1 ) - 1;
    //    var maxCharCount = 3 * needPartCount;
    //    var maxSlot = startIndex + 2;//(slotCount - (3 - list.Count));
    //    for ( int i = startIndex; i <= maxSlot; i++ )
    //    {
    //        var s1Length = i - strStartIndex;
    //        var firstChar = s[strStartIndex];
    //        if ( 3 < s1Length ) break;

    //        var restCount = sLength - i;
    //        if ( maxCharCount < restCount ) continue;
    //        if ( restCount < needPartCount ) break;

    //        if (1 < s1Length && firstChar == '0') break;
    //        if (3 == s1Length)
    //        {
    //            if ('2' < firstChar) break;

    //            var secondChar = s[strStartIndex + 1];
    //            var thirdChar = s[strStartIndex + 2];

    //            if (firstChar == '2' && ('5' < secondChar || ('5' == secondChar && '5' < thirdChar))) break;
    //        }

    //        //var pass = CheckIPPart(s, strStartIndex, s1Length);
    //        //if (!pass) break;

    //        if( needPartCount == 1 )
    //        {
    //            if( CheckIPPart(s, i, restCount ) )
    //            {
    //                var index1 = list[1];
    //                var ip = $"{s.Substring(0, index1)}.{s.Substring(index1, strStartIndex - index1)}.{s.Substring(strStartIndex, s1Length)}.{s.Substring(i, restCount)}";
    //                ret.Add(ip);
    //            }
    //            continue;
    //        }

    //        list.Add(i);

    //        BackTrack( s, slotCount, i + 1, list, ret );

    //        list.RemoveAt(list.Count - 1);
    //    }
    //}

    //private static bool CheckIPPart(string s, int startIndex, int count)
    //{
    //    var s1Length = count;
    //    var firstChar = s[startIndex];
    //    if ( 3 < s1Length ) return false;

    //    if (1 < s1Length && firstChar == '0') return false;
    //    //if ( list.Count == 1 && firstChar == '0' ) break;
    //    //if ( s1Length == 1 && firstChar == '0') break;
    //    if (3 == s1Length)
    //    {
    //        if ('2' < firstChar) return false;

    //        var secondChar = s[startIndex + 1];
    //        var thirdChar = s[startIndex + 2];

    //        if (firstChar == '2' && ('5' < secondChar || ('5' == secondChar && '5' < thirdChar))) return false;
    //    }

    //    return true;
    //}
}

/*

复原IP地址
力扣 (LeetCode)
发布于 1 年前
33.8k
直觉
最朴素的解法是暴力法,
换而言之，检查点可能的所有位置，并只保留有效的部分。在最坏的情况下，有11个可能的位置，因此需要11 \times 10 \times 9 = 99011×10×9=990 次检查。

可以通过以下两个概念来优化。

第一个概念是 约束规划。

这意味着对每个点的放置设置一些限制。若已经放置了一个点，下一个点只有 3 种可能：1/2/3个数字之后。

这样做传播了_约束_ ，且减少了需要考虑的情况。我们只需要检测 3 \times 3 \times 3 = 273×3×3=27种情况，而非f 990990 种。

第二个概念是 回溯。

我们假设已经放置了一或两个点使得无法摆放其他点来生成有效IP地址。这时应该做什么？ 回溯。T也就是说，回到之前，改变上一个摆放点的位置。并试着继续。如果依然不行，则继续 回溯。




方法一 ： 回溯(DFS)
这是一个回溯函数backtrack(prev_pos = -1, dots = 3) 的算法，该函数使用上一个放置的点 prev_pos
和待放置点的数量 dots 两个参数 :

遍历三个有效位置curr_pos 以放置点。
检查从上一个点到现在点中间的部分是否有效 :
是 :
放置该点。
检查全部 3个点是否放好:
是 :
将结果添加到输出列表中。
否 :
继续放下一个点 backtrack(curr_pos, dots - 1)。
回溯，移除最后一个点。


class Solution {
  int n;
  String s;
  LinkedList<String> segments = new LinkedList<String>();
  ArrayList<String> output = new ArrayList<String>();

  public boolean valid(String segment) {
    
    //Check if the current segment is valid :
    //1. less or equal to 255      
    //2. the first character could be '0' 
    //only if the segment is equal to '0'
    
    int m = segment.length();
    if (m > 3)
      return false;
    return (segment.charAt(0) != '0') ? (Integer.valueOf(segment) <= 255) : (m == 1);
  }

  public void update_output(int curr_pos) {
    
    //Append the current list of segments 
    //to the list of solutions
    
    String segment = s.substring(curr_pos + 1, n);
    if (valid(segment)) {
      segments.add(segment);
      output.add(String.join(".", segments));
      segments.removeLast();
    }
  }

  public void backtrack(int prev_pos, int dots) {
    
    //prev_pos : the position of the previously placed dot
    //dots : number of dots to place
    
    // The current dot curr_pos could be placed 
    // in a range from prev_pos + 1 to prev_pos + 4.
    // The dot couldn't be placed 
    // after the last character in the string.
    int max_pos = Math.min(n - 1, prev_pos + 4);
    for (int curr_pos = prev_pos + 1; curr_pos < max_pos; curr_pos++) {
      String segment = s.substring(prev_pos + 1, curr_pos + 1);
      if (valid(segment)) {
        segments.add(segment);  // place dot
        if (dots - 1 == 0)      // if all 3 dots are placed
          update_output(curr_pos);  // add the solution to output
        else
          backtrack(curr_pos, dots - 1);  // continue to place dots
        segments.removeLast();  // remove the last placed dot 
      }
    }
  }

  public List<String> restoreIpAddresses(String s) {
    n = s.length();
    this.s = s;
    backtrack(-1, 3);
    return output;
  }
}
复杂度分析

时间复杂度 : 如上文所述，需要检查的组合不多于27个。
空间复杂度 : 常数空间存储解，不多于19 个有效IP地址。
image.png
下一篇：使用递归解决“93. 复原IP地址”（C语言）

public class Solution {
    private int[] ipAddr = new int[4];
    private List<string> ans = new List<string>();
    public IList<string> RestoreIpAddresses(string s)
    {
        DFS(0, 0, s);
        return ans;
    }

    private void DFS(int ipIndex, int sIndex, string s)
    {
        if (ipIndex >= 4)
        {
            if (sIndex >= s.Length)
            {
                ans.Add(ToString(ipAddr));
            }
            return;
        }

        for (int i = sIndex; i < sIndex + 3; i++)
        {
            var num = ToIpNumber(s, sIndex, i);
            if (num >= 0)
            {
                ipAddr[ipIndex] = num;
                DFS(ipIndex + 1, i + 1, s);
            }
        }
    }

    private string ToString(int[] ipAddr)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < ipAddr.Length; i++)
        {
            if (i != 0)
            {
                sb.Append('.');
            }

            sb.Append(ipAddr[i]);
        }

        return sb.ToString();
    }

    private int ToIpNumber(string s, int start, int end)
    {
        if (end >= s.Length)
        {
            return -1;
        }
        var len = end - start + 1;
        if (len < 1 || len > 3)
        {
            return -1;
        }
        if (len != 1 && s[start] == '0')
        {
            return -1;
        }

        int num = Convert.ToInt32(s.Substring(start, len));
        if (num < 0 || num > 255)
        {
            return -1;
        }

        return num;
    }
}




*/
