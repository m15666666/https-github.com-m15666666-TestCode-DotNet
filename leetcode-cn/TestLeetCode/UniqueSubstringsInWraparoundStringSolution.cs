using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
把字符串 s 看作是“abcdefghijklmnopqrstuvwxyz”的无限环绕字符串，所以 s 看起来是这样的："...zabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcd....". 
现在我们有了另一个字符串 p 。你需要的是找出 s 中有多少个唯一的 p 的非空子串，尤其是当你的输入是字符串 p ，你需要输出字符串 s 中 p 的不同的非空子串的数目。 
注意: p 仅由小写的英文字母组成，p 的大小可能超过 10000。

示例 1:

输入: "a"
输出: 1
解释: 字符串 S 中只有一个"a"子字符。
 
示例 2:

输入: "cac"
输出: 2
解释: 字符串 S 中的字符串“cac”只有两个子串“a”、“c”。.
 
示例 3:

输入: "zab"
输出: 6
解释: 在字符串 S 中有六个子串“z”、“a”、“b”、“za”、“ab”、“zab”。. 
*/
/// <summary>
/// https://leetcode-cn.com/problems/unique-substrings-in-wraparound-string/
/// 467. 环绕字符串中唯一的子字符串
/// https://blog.csdn.net/zrh_CSDN/article/details/84844224
/// </summary>
class UniqueSubstringsInWraparoundStringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));b
    }

    public int FindSubstringInWraproundString(string p)
    {
        if (string.IsNullOrEmpty(p)) return 0;

        int[] counts = new int[26];
        Array.Fill(counts, 0);
        int count = 0;
        for (int i = 0; i < p.Length; ++i)
        {
            var c = p[i];
            if (i > 0 && (c == p[i - 1] + 1 || p[i - 1] - c == 25)) ++count;
            else count = 1;

            const char A = 'a';
            var index = c - A;
            if (counts[index] < count) counts[index] = count;
        }

        return counts.Sum();
    }
}