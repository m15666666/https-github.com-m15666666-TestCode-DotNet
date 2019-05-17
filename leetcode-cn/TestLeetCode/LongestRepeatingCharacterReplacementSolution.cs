using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给你一个仅由大写英文字母组成的字符串，你可以将任意位置上的字符替换成另外的字符，总共可最多替换 k 次。在执行上述操作后，找到包含重复字母的最长子串的长度。

注意:
字符串长度 和 k 不会超过 104。

示例 1:

输入:
s = "ABAB", k = 2

输出:
4

解释:
用两个'A'替换为两个'B',反之亦然。
示例 2:

输入:
s = "AABABBA", k = 1

输出:
4

解释:
将中间的一个'A'替换为'B',字符串变为 "AABBBBA"。
子串 "BBBB" 有最长重复字母, 答案为 4。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/longest-repeating-character-replacement/
/// 424. 替换后的最长重复字符
/// https://www.cnblogs.com/kexinxin/p/10242222.html
/// </summary>
class LongestRepeatingCharacterReplacementSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int CharacterReplacement(string s, int k)
    {
        if (string.IsNullOrWhiteSpace(s)) return 0;
        int start = 0;
        int end = 0;
        int res = 0;
        int[] charNum = new int[26];
        charNum[s[0] - 'A']++;
        while (end < s.Length)
        {
            int maxChar = 0;
            for (int i = 0; i < 26; i++)
            {
                if (charNum[i] > maxChar)
                    maxChar = charNum[i];
            }
            if (maxChar + k > end - start)
            {
                end++;
                if (end < s.Length)
                    charNum[s[end] - 'A']++;
            }
            else
            {
                charNum[s[start] - 'A']--;
                start++;
            }
            if (maxChar + k > res)
                res = maxChar + k <= s.Length ? maxChar + k : s.Length;
        }
        return res;
    }
}