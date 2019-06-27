using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个字符串和一个字符串字典，找到字典里面最长的字符串，该字符串可以通过删除给定字符串的某些字符来得到。
如果答案不止一个，返回长度最长且字典顺序最小的字符串。如果答案不存在，则返回空字符串。

示例 1:

输入:
s = "abpcplea", d = ["ale","apple","monkey","plea"]

输出: 
"apple"
示例 2:

输入:
s = "abpcplea", d = ["a","b","c"]

输出: 
"a"
说明:

所有输入的字符串只包含小写字母。
字典的大小不会超过 1000。
所有输入的字符串长度不会超过 1000。
*/
/// <summary>
/// https://leetcode-cn.com/problems/longest-word-in-dictionary-through-deleting/
/// 524. 通过删除字母匹配到字典里最长单词
/// </summary>
class LongestWordInDictionaryThroughDeletingSolution
{
    public void Test()
    {
        var ret = FindLongestWord("bab", new string[] { "ba", "ab", "a", "b" });
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string FindLongestWord(string s, IList<string> d)
    {
        string[] dic = d?.Where(item => !string.IsNullOrEmpty(item)).OrderBy( item => item ).ToArray();
        if (string.IsNullOrEmpty(s) || dic == null || dic.Length == 0) return string.Empty;
        int[] indexs = new int[dic.Length];
        Array.Fill(indexs, 0);
        int matchCount = 0;
        foreach( var c in s)
        {
            for( int i = 0; i < dic.Length; i++ )
            {
                var text = dic[i];
                var index = indexs[i];
                if( index < text.Length && c == text[index] )
                {
                    indexs[i] = index + 1;
                    if (indexs[i] == text.Length && matchCount < text.Length)
                    {
                        matchCount = text.Length;
                    }
                }
            }
        }

        if (matchCount == 0) return string.Empty;
        for (int i = 0; i < dic.Length; i++)
            if (indexs[i] == dic[i].Length && indexs[i] == matchCount) return dic[i];
        return string.Empty;
    }
}