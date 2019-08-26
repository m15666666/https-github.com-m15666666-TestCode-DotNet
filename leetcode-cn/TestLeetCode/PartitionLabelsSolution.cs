using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
字符串 S 由小写字母组成。我们要把这个字符串划分为尽可能多的片段，同一个字母只会出现在其中的一个片段。返回一个表示每个字符串片段的长度的列表。

示例 1:

输入: S = "ababcbacadefegdehijhklij"
输出: [9,7,8]
解释:
划分结果为 "ababcbaca", "defegde", "hijhklij"。
每个字母最多出现在一个片段中。
像 "ababcbacadefegde", "hijhklij" 的划分是错误的，因为划分的片段数较少。
注意:

S的长度在[1, 500]之间。
S只包含小写字母'a'到'z'。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/partition-labels/
/// 763. 划分字母区间
/// https://blog.csdn.net/qq_21895079/article/details/82898203
/// </summary>
class PartitionLabelsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public IList<int> PartitionLabels(string S)
    {
        if (S == null || S.Length == 0) return new int []{ 0 };
        int len = S.Length;
        if(len == 1) return new int[] { 1 };

        Dictionary<char, int> lastPositions = new Dictionary<char, int>();
        for( int index = len - 1; -1 < index; index--)
        {
            var c = S[index];
            if (lastPositions.ContainsKey(c)) continue;
            lastPositions.Add(c, index);
        }

        int i = 0;
        var ret = new List<int>();
        while (i < len) {
            var start = i;
            var end = lastPositions[S[i]];
            for (int j = start; j < len; j++)
            {
                var last = lastPositions[S[j]];
                if (end < last) end = last;
                else if (j == end)
                {
                    ret.Add(end - start + 1);
                    i = end + 1;
                    break;
                }
            }
        }
        return ret;
    }
}