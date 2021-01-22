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
        const char a = 'a';

        int[] rec = new int[26];
        Array.Fill(rec, 0);
        for (int i = start; i < end; i++)
            ++rec[str[i] - a];
        int p = start;
        for (int i = start; i < end; i++)
            if (rec[str[i] - a] < k)
            {
                max = Math.Max(max, resolve(p, i));
                p = i + 1;
            }
        if (p == start) return end - start;
        return Math.Max(max, resolve(p, end));
    }
}
/*
Java 递归 0ms
Gyy
发布于 2020-12-30
607
关键在于，如果一个子串包含整个串中不满k个的字符，那这个子串就一定不满足条件

如果一个串中没有这样的字符，那就直接返回它的长度即可
如果有，那就以这个字符划分子串，递归判断子串是否满足。

综合来说，复杂度是O(n)


class Solution {
    public int longestSubstring(String s, int k) {
        int res = 0;
        boolean[] m = new boolean[26];
        int[] mid = new int[26];
        for (int i = 0; i < s.length(); i++) {
            mid[s.charAt(i) - 'a']++;
        }
        for (int i = 0; i < mid.length; i++) {
            if (mid[i] < k) {
                m[i] = true;
            }
        }
        int start = 0;
        boolean f = true;
        for (int i = 0; i < s.length(); i++) {
            if (m[s.charAt(i) - 'a']) {
                f = false;
                if (start < i) {
                    res = Integer.max(res, longestSubstring(s.substring(start, i), k));
                }
                start = i + 1;
            }
        }
        if (f) {
            res = s.length();
        } else {
            if(start < s.length()) {
                res = Integer.max(res, longestSubstring(s.substring(start, s.length()), k));
            }
        }
        return res;
    }
}
 
*/