using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
给定一个字符串数组 words，找到 length(word[i]) * length(word[j]) 的最大值，并且这两个单词不含有公共字母。你可以认为每个单词只包含小写字母。如果不存在这样的两个单词，返回 0。

示例 1:

输入: ["abcw","baz","foo","bar","xtfn","abcdef"]
输出: 16 
解释: 这两个单词为 "abcw", "xtfn"。
示例 2:

输入: ["a","ab","abc","d","cd","bcd","abcd"]
输出: 4 
解释: 这两个单词为 "ab", "cd"。
示例 3:

输入: ["a","aa","aaa","aaaa"]
输出: 0 
解释: 不存在这样的两个单词。 
*/
/// <summary>
/// https://leetcode-cn.com/problems/maximum-product-of-word-lengths/
/// 318. 最大单词长度乘积
/// 
/// </summary>
class MaximumProductOfWordLengthsSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public int MaxProduct(string[] words)
    {
        if (words == null || words.Length < 2) return 0;
        int ret = 0;
        var wordDescriptions = words.Select(word => new WordDescription(word)).OrderByDescending(wordDescription => wordDescription.Length).ToArray();
        bool containSameChar = false;
        for (int i = 0; i < wordDescriptions.Length - 1; i++)
        {
            var chars = wordDescriptions[i].Chars;
            for (int j = i + 1; j < wordDescriptions.Length; j++)
            {
                if ((chars & wordDescriptions[j].Chars) == 0)
                {
                    var len = wordDescriptions[i].Length * wordDescriptions[j].Length;
                    if (!containSameChar) return len;
                    if (ret < len) ret = len;
                }
                else containSameChar = true;
            }
        }
        return ret;
    }
    class WordDescription
    {
        public int Length { get; set; }
        public int Chars { get; set; }
        public WordDescription( string word )
        {
            if (word == null) return;
            Length = word.Length;
            foreach( var c in word)
            {
                var offset = c - 'a';
                Chars |= (1 << offset);
            }
        }
    }
}
/*
public class Solution {
    public  int MaxProduct(string[] words)
    {
        var arr = new int[26];
        var words_bt = new int[words.Length];
        for (var i = 0; i < words.Length; i++)
            words_bt[i] = _s2b(arr, words[i]);
        var max = 0;
        for (var i = 0; i < words.Length-1; i++) {
            for (var j = i + 1; j < words.Length; j++) {
                if ((words_bt[i] & words_bt[j]) == 0)
                    max = Math.Max(max,words[i].Length*words[j].Length);
            }
        }
        return max;
    }
    private  int _s2b(int[] arr, string s)
    {
        for (var i = 0; i < arr.Length; i++)
        {
            arr[i] = 0;
        }
        foreach (var v in s)
        {
            arr[v - 'a'] = 1;
        }
        var res = 0;
        for (var i = 0; i < arr.Length; i++)
        {
            res += (int)Math.Pow(2, i) * arr[i];
        }
        return res;
    }
} 
*/
