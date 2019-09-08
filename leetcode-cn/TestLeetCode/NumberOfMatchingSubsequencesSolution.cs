using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定字符串 S 和单词字典 words, 求 words[i] 中是 S 的子序列的单词个数。

示例:
输入: 
S = "abcde"
words = ["a", "bb", "acd", "ace"]
输出: 3
解释: 有三个是 S 的子序列的单词: "a", "acd", "ace"。
注意:

所有在words和 S 里的单词都只由小写字母组成。
S 的长度在 [1, 50000]。
words 的长度在 [1, 5000]。
words[i]的长度在[1, 50]。
*/
/// <summary>
/// https://leetcode-cn.com/problems/number-of-matching-subsequences/
/// 792. 匹配子序列的单词数
/// https://blog.csdn.net/start_lie/article/details/85072650
/// </summary>
class NumberOfMatchingSubsequencesSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int NumMatchingSubseq(string S, string[] words)
    {
        const int a = 'a';

        List<ushort>[] charIndexes = new List<ushort>[26];
        for (int i = 0; i < charIndexes.Length; i++) charIndexes[i] = new List<ushort>();

        ushort index = 0;
        foreach (char c in S) charIndexes[c - a].Add(index++);

        int ret = 0;
        HashSet<string> exists = new HashSet<string>();
        foreach (var word in words)
        {
            if (exists.Contains(word))
            {
                ret ++;
                continue;
            }

            int p = -1;
            bool found = true;
            foreach (char c in word )
            {
                var list = charIndexes[c - a];
                if (list.Count == 0 || list[list.Count - 1] <= p)
                {
                    found = false;
                    break;
                }

                if (p < list[0]) p = list[0];
                else
                {
                    ushort left = 0, right = (ushort)(list.Count - 1);
                    while(left < right)
                    {
                        ushort mid = (ushort)((left + right) / 2);
                        var v = list[mid];
                        if (v <= p) left = ++mid;
                        else right = mid;
                    }
                    p = list[left];
                }
            }
            if (found)
            {
                exists.Add(word);
                ret++;
            }
        }
        return ret;
    }
}