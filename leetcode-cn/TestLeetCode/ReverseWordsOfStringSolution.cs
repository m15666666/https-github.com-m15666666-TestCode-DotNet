using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// https://leetcode-cn.com/problems/reverse-words-in-a-string/
/// 151. 翻转字符串里的单词
/// 给定一个字符串，逐个翻转字符串中的每个单词。
/// 示例:  
/// 输入: "the sky is blue",
/// 输出: "blue is sky the".
/// </summary>
class ReverseWordsOfStringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }


    public string ReverseWords(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return string.Empty;

        var parts = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Array.Reverse(parts);
        return string.Join(' ', parts);
    }

}