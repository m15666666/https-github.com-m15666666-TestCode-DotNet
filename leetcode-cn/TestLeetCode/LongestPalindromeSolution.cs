using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/longest-palindromic-substring/description/
/// 最长回文子串
/// 给定一个字符串 s，找到 s 中最长的回文子串。你可以假设 s 的最大长度为1000。
/// </summary>
class LongestPalindromeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    
    public string LongestPalindrome(string s)
    {
        if (s == null || s.Length == 1) return s;
        string lastPalindrome = string.Empty;
        int lastPalindromeLength = lastPalindrome.Length;

        for ( int startIndex = 0; startIndex < s.Length; startIndex++)
        {
            int endIndex = s.Length - 1;
            if (endIndex - startIndex + 1 <= lastPalindromeLength) break;

            while (true)
            {
                var remainLength = endIndex - startIndex + 1;
                if (remainLength <= lastPalindromeLength) break;

                if (s[startIndex] == s[endIndex])
                {
                    var isPalin = IsPalindrome(s, startIndex, endIndex);
                    if (isPalin)
                    {
                        lastPalindrome = s.Substring(startIndex, remainLength);
                        lastPalindromeLength = lastPalindrome.Length;
                        break;
                    }
                }

                endIndex--;
                if (endIndex < startIndex) break;
            }
        }

        return lastPalindrome;
    }

    /// <summary>
    /// 是回文子串
    /// </summary>
    /// <param name="s"></param>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    /// <returns></returns>
    private static bool IsPalindrome( string s, int startIndex, int endIndex )
    {
        if (s == null || s.Length == 1) return true;
        while (startIndex < endIndex )
        {
            if (s[startIndex++] != s[endIndex--]) return false;
        }

        return true;
    }
}