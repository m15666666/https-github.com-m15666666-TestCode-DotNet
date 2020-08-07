using System;

/*
比较两个版本号 version1 和 version2。
如果 version1 > version2 返回 1，如果 version1 < version2 返回 -1， 除此之外返回 0。

你可以假设版本字符串非空，并且只包含数字和 . 字符。

 . 字符不代表小数点，而是用于分隔数字序列。

例如，2.5 不是“两个半”，也不是“差一半到三”，而是第二版中的第五个小版本。

你可以假设版本号的每一级的默认修订版号为 0。例如，版本号 3.4 的第一级（大版本）和第二级（小版本）修订号分别为 3 和 4。其第三级和第四级修订号均为 0。
 

示例 1:

输入: version1 = "0.1", version2 = "1.1"
输出: -1
示例 2:

输入: version1 = "1.0.1", version2 = "1"
输出: 1
示例 3:

输入: version1 = "7.5.2.4", version2 = "7.5.3"
输出: -1
示例 4：

输入：version1 = "1.01", version2 = "1.001"
输出：0
解释：忽略前导零，“01” 和 “001” 表示相同的数字 “1”。
示例 5：

输入：version1 = "1.0", version2 = "1.0.0"
输出：0
解释：version1 没有第三级修订号，这意味着它的第三级修订号默认为 “0”。
 

提示：

版本字符串由以点 （.） 分隔的数字字符串组成。这个数字字符串可能有前导零。
版本字符串不以点开始或结束，并且其中不会有两个连续的点。

*/

/// <summary>
/// https://leetcode-cn.com/problems/compare-version-numbers/
/// 165. 比较版本号
/// 比较两个版本号 version1 和 version2。
/// 如果 version1 > version2 返回 1，如果 version1<version2 返回 -1， 除此之外返回 0。
/// 你可以假设版本字符串非空，并且只包含数字和 . 字符。
/// . 字符不代表小数点，而是用于分隔数字序列。
/// 例如，2.5 不是“两个半”，也不是“差一半到三”，而是第二版中的第五个小版本。
/// 示例 1:
/// 输入: version1 = "0.1", version2 = "1.1"
/// 输出: -1
/// 示例 2:
/// 输入: version1 = "1.0.1", version2 = "1"
/// 输出: 1
/// 示例 3:
/// 输入: version1 = "7.5.2.4", version2 = "7.5.3"
/// 输出: -1
/// </summary>
internal class CompareVersionNumbersSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int CompareVersion(string version1, string version2)
    {
        int n1 = version1.Length, n2 = version2.Length;
        int i1, i2;
        int p1 = 0, p2 = 0;
        while (p1 < n1 || p2 < n2)
        {
            (i1, p1) = GetNextNumber(version1, n1, p1);
            (i2, p2) = GetNextNumber(version2, n2, p2);
            if (i1 != i2) return i1 > i2 ? 1 : -1;
        }
        return 0;

        (int, int) GetNextNumber(string version, int n, int p)
        {
            if (n <= p) return (0, p);

            int number, pEnd = p;
            while (pEnd < n && version[pEnd] != '.') ++pEnd;

            number = int.Parse(version.AsSpan(p, pEnd - p));
            p = pEnd + 1;

            return (number, p);
        }
    }

    //public int CompareVersion(string version1, string version2)
    //{
    //    const string dot = ".";
    //    var parts1 = version1.Split(dot, StringSplitOptions.RemoveEmptyEntries);
    //    var parts2 = version2.Split(dot, StringSplitOptions.RemoveEmptyEntries);

    //    var minLenth = Math.Min(parts1.Length, parts2.Length);
    //    for( int index = 0; index < minLenth; index++)
    //    {
    //        var num1 = int.Parse(parts1[index]);
    //        var num2 = int.Parse(parts2[index]);
    //        if (num2 < num1) return 1;
    //        else if (num1 < num2) return -1;
    //    }

    //    if (parts1.Length == parts2.Length) return 0;

    //    var parts = minLenth < parts1.Length ? parts1 : parts2;
    //    var result = minLenth < parts1.Length ? 1 : -1;
    //    for (int index = minLenth; index < parts.Length; index++)
    //        if (0 < int.Parse(parts[index])) return result;
    //    return 0;
    //}
}

/*

比较版本号
力扣 (LeetCode)
发布于 2020-04-02
4.8k
方法一：分割+解析，两次遍历，线性空间
第一个想法是将两个字符串按点字符分割成块，然后逐个比较这些块。

在这里插入图片描述
如果两个版本号的块数相同，则可以有效工作。如果不同，则需要在较短字符串末尾补充相应的 .0 块数使得块数相同。

在这里插入图片描述

算法：

根据点分割两个字符串将分割的结果存储到数组中。
遍历较长数组并逐个比较块。如果其中一个数组结束了，实际上可以根据需要添加尽可能多的零，以继续与较长的数组进行比较。
如果两个版本号不同，则返回 1 或 -1。
版本号相同，返回 0。

class Solution {
  public int compareVersion(String version1, String version2) {
    String[] nums1 = version1.split("\\.");
    String[] nums2 = version2.split("\\.");
    int n1 = nums1.length, n2 = nums2.length;

    // compare versions
    int i1, i2;
    for (int i = 0; i < Math.max(n1, n2); ++i) {
      i1 = i < n1 ? Integer.parseInt(nums1[i]) : 0;
      i2 = i < n2 ? Integer.parseInt(nums2[i]) : 0;
      if (i1 != i2) {
        return i1 > i2 ? 1 : -1;
      }
    }
    // the versions are equal
    return 0;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(N + M + \max(N, M))O(N+M+max(N,M))。其中 NN 和 MM 指的是输入字符串的长度。
空间复杂度：\mathcal{O}(N + M)O(N+M)，使用了两个数组 nums1 和 nums2 存储两个字符串的块。
方法二：双指针，一次遍历，常数空间
方法一有两个缺点：

是两次遍历的解决方法。
消耗线性空间。
我们能否实现一个只有一次遍历和消耗常数空间的解决方法呢？

其思想是在每个字符串上使用两个指针，跟踪每个数组的开始和结束。

这样，可以并行地沿着两个字符串移动，检索并比较相应的块。一旦两个字符串都被解析，比较也就完成了。

算法：

首先，我们定义了一个名为 get_next_chunk(version, n, p) 的函数，用于检索字符串中的下一个块。这个函数有三个参数：输入字符串 version，它的长度 n，以及指针 p 为要检索块的第一个字符。它在指针 p 和下一个点之间返回一个整数块。为了帮助迭代，返回的是下一个快的第一个字符的指针。下面是如何使用此函数解决问题的方法：

指针 p1 和 p2 分别指向 version1 和 version2 的起始位置：p1=p2=0。
并行遍历两个字符串。当 p1 < n1 or p2 < n2：
使用 get_next_chunk 函数获取 version1 和 version2 的下一个块 i1 和 i2。
比较 i1 和 i2。如果不相同，则返回 1 或 -1。
如果到了这里，说明版本号相同，则返回 0。
下面实现 get_next_chunk(version, n, p) 函数：

块的开头由指针 p 标记。如果 p 设置为字符串的结尾，则字符串解析完成。若要继续比较，则在添加 .0 返回。
如果 p 不在字符串的末尾，则沿字符串移动指针 p_end 以查找块的结尾。
返回块 version.substring(p, p_end)。



class Solution {
  public Pair<Integer, Integer> getNextChunk(String version, int n, int p) {
    // if pointer is set to the end of string
    // return 0
    if (p > n - 1) {
      return new Pair(0, p);
    }
    // find the end of chunk
    int i, pEnd = p;
    while (pEnd < n && version.charAt(pEnd) != '.') {
      ++pEnd;
    }
    // retrieve the chunk
    if (pEnd != n - 1) {
      i = Integer.parseInt(version.substring(p, pEnd));
    } else {
      i = Integer.parseInt(version.substring(p, n));
    }
    // find the beginning of next chunk
    p = pEnd + 1;

    return new Pair(i, p);
  }

  public int compareVersion(String version1, String version2) {
    int p1 = 0, p2 = 0;
    int n1 = version1.length(), n2 = version2.length();

    // compare versions
    int i1, i2;
    Pair<Integer, Integer> pair;
    while (p1 < n1 || p2 < n2) {
      pair = getNextChunk(version1, n1, p1);
      i1 = pair.getKey();
      p1 = pair.getValue();

      pair = getNextChunk(version2, n2, p2);
      i2 = pair.getKey();
      p2 = pair.getValue();
      if (i1 != i2) {
        return i1 > i2 ? 1 : -1;
      }
    }
    // the versions are equal
    return 0;
  }
}
复杂度分析

时间复杂度：\mathcal{O}(\max(N, M))O(max(N,M))。其中 NN 和 MM 指的是输入字符串的长度。
空间复杂度：\mathcal{O}(1)O(1)，没有使用额外的数据结构。
下一篇：模拟


public class Solution {
    public int CompareVersion(string version1, string version2) {
           int p1 = 0, p2 = 0;
            int end = Math.Max(version1.Length, version2.Length);
            while (p1 < end || p2 < end)
            {
                int v1 = 0, v2 = 0;
                while (p1 < version1.Length && version1[p1] != '.')
                {
                    v1 = v1 * 10 + version1[p1] - '0';
                    p1++;
                }
                while (p2 < version2.Length && version2[p2] != '.')
                {
                    v2 = v2 * 10 + version2[p2] - '0';
                    p2++;
                }
                if (v1 > v2) return 1;
                if (v1 < v2) return -1;
                p1++;
                p2++;
            }
            return 0;
    }
}

public class Solution {
    public int CompareVersion(string version1, string version2) {

        int i;
        string[] v1 = version1.Split('.');
        string[] v2 = version2.Split('.');

        int len1 = v1.Length;
        int len2 = v2.Length;

        if (len1 > len2)
        {
            for (i=0;i<len2;i++)
            {
                if (Convert.ToInt32(v1[i]) > Convert.ToInt32(v2[i]))
                    return 1;
                else if (Convert.ToInt32(v1[i]) < Convert.ToInt32(v2[i]))
                    return -1;
            }
            for (i=len2;i<len1;i++)
            {
                if ( Convert.ToInt32(v1[i]) > 0)
                    return 1;
            }
        }
        else
        {
            for (i=0;i<len1;i++)
            {
                if (Convert.ToInt32(v1[i]) > Convert.ToInt32(v2[i]))
                    return 1;
                else if (Convert.ToInt32(v1[i]) < Convert.ToInt32(v2[i]))
                    return -1;
            }
            for (i=len1;i<len2;i++)
            {
                if (Convert.ToInt32(v2[i]) > 0)
                    return -1;
            }
        }
        return 0;
    }
}

public class Solution
{
    public int CompareVersion (string version1, string version2)
    {
        var str1 = version1.Split (".");
        var str2 = version2.Split (".");
        str1[0] = TrimStartZero (str1[0]);
        str2[0] = TrimStartZero (str2[0]);
        var index1 = 0;
        var index2 = 0;
        int a = 0;
        int b = 0;
        while (index1 < str1.Length && index2 < str2.Length)
        {
            a = Convert.ToInt32 (str1[index1]);
            b = Convert.ToInt32 (str2[index2]);
            if (a == b)
            {
                index2++;
                index1++;
            }
            else if (a < b)
            {
                return -1;
            }
            else
            {
                return 1;
            }

        }
        while (index1 < str1.Length)
        {
            if (Convert.ToInt32 (str1[index1]) > 0)
            {
                return 1;
            }
            index1++;
        }
        while (index2 < str2.Length)
        {
            if (Convert.ToInt32 (str2[index2]) > 0)
            {
                return -1;
            }
            index2++;
        }
        return 0;
    }
    public string TrimStartZero (string s)
    {
        if (s.Length < 2)
        {
            return s;
        }
        var index = 0;
        while (index <s.Length)
        {
            if (s[index] == '0')
            {
                index++;
            }
            else
            {
                break;
            }
        }
        if (index > 0)
        {
            s = s.Substring (index, s.Length - index);
        }
  
        return s;
    }

}
*/