using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
找到给定字符串（由小写字符组成）中的最长子串 T ， 要求 T 中的每一字符出现次数都不少于 k 。输出 T 的长度。

示例 1:

输入:
s = "aaabb", k = 3

输出:
3

最长子串为 "aaa" ，其中 'a' 重复了 3 次。
示例 2:

输入:
s = "ababbc", k = 2

输出:
5

最长子串为 "ababb" ，其中 'a' 重复了 2 次， 'b' 重复了 3 次。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/longest-substring-with-at-least-k-repeating-characters/
/// 395. 至少有K个重复字符的最长子串
/// https://blog.csdn.net/start_lie/article/details/85129826
/// </summary>
class LongestSubstringWithAtLeastKRepeatingCharactersSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int LongestSubstring(string s, int k)
    {
        if (s.Length < k) return 0;
        if (k < 2) return s.Length;
        this.str = s.ToCharArray();
        this.k = k;
        return resolve(0, s.Length);
    }

    private char[] str = null;
    private int k = 0;
    private int max = 0;

    private int resolve(int start, int end)
    {
        if (end <= start) return 0;

        int[] rec = new int[26];
        Array.Fill(rec, 0);
        for (int i = start; i < end; i++)
            ++rec[str[i] - 97];
        int p = start;
        for (int i = start; i < end; i++)
            if (rec[str[i] - 97] < k)
            {
                max = Math.Max(max, resolve(p, i));
                p = i + 1;
            }
        if (p == start) return end - start;
        return Math.Max(max, resolve(p, end));
    }
}