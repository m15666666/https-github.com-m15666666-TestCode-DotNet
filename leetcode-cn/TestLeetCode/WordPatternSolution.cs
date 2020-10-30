using System;
using System.Collections.Generic;

/*
给定一种规律 pattern 和一个字符串 str ，判断 str 是否遵循相同的规律。

这里的 遵循 指完全匹配，例如， pattern 里的每个字母和字符串 str 中的每个非空单词之间存在着双向连接的对应规律。

示例1:

输入: pattern = "abba", str = "dog cat cat dog"
输出: true
示例 2:

输入:pattern = "abba", str = "dog cat cat fish"
输出: false
示例 3:

输入: pattern = "aaaa", str = "dog cat cat dog"
输出: false
示例 4:

输入: pattern = "abba", str = "dog dog dog dog"
输出: false
说明:
你可以假设 pattern 只包含小写字母， str 包含了由单个空格分隔的小写字母。    

*/

/// <summary>
/// https://leetcode-cn.com/problems/word-pattern/
/// 290. 单词规律
///
///
/// </summary>
internal class WordPatternSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool WordPattern(string pattern, string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return string.IsNullOrEmpty(pattern);

        var words = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (pattern.Length != words.Length) return false;
        int len = words.Length;
        string[] char2Word = new string[26];
        HashSet<string> wordSet = new HashSet<string>(len);
        for (int i = 0; i < len; ++i)
        {
            var patternKey = pattern[i] - 'a';
            var word = words[i];
            if (char2Word[patternKey] != null)
            {
                if (!string.Equals(char2Word[patternKey], word)) return false;
                continue;
            }
            if (wordSet.Contains(word)) return false;
            char2Word[patternKey] = word;
            wordSet.Add(word);
        }
        return true;
    }

    //public bool WordPattern(string pattern, string s) {
    //    if (string.IsNullOrWhiteSpace(s)) return string.IsNullOrEmpty(pattern);

    //    var words = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    //    if (pattern.Length != words.Length) return false;
    //    int len = words.Length;
    //    Dictionary<string, char> word2pattern = new Dictionary<string, char>();
    //    HashSet<char> pattern2word = new HashSet<char>();
    //    for( int i = 0; i < len; ++i )
    //    {
    //        var patternKey = pattern[i];
    //        var word = words[i];
    //        if (!word2pattern.ContainsKey(word))
    //        {
    //            if (pattern2word.Contains(patternKey)) return false;

    //            pattern2word.Add(patternKey);
    //            word2pattern[word] = patternKey;
    //        }
    //        else if (patternKey != word2pattern[word]) return false;
    //    }
    //    return true;
    //}
}

/*
public class Solution {
    public bool WordPattern(string pattern, string s) {
        int m = pattern.Length;
        string[] strs = s.Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries);
        if(strs.Length  != m) return false;
        Dictionary<char,string> wordDic = new Dictionary<char,string>();
        for(int i=0;i<m;i++)
        {
            if(wordDic.ContainsKey(pattern[i]))
            {
                if(wordDic[pattern[i]] != strs[i])
                {
                    return false;
                }
            }
            else
            {
                if(wordDic.ContainsValue(strs[i]))
                {
                    return false;
                }
                wordDic[pattern[i]] = strs[i];
            }
        }
        return true;
    }
}

public class Solution {
    public bool WordPattern(string pattern, string s){
           string[] array= s.Split(' ');
           if (array.Length != pattern.Length)
                return false;
            HashSet<string> hs = new HashSet<string>();
            Dictionary<char, string> dic = new Dictionary<char, string>();
            for (int i = 0; i < array.Length; i++)
            {
                if (dic.ContainsKey(pattern[i]))
                {
                    if (dic[pattern[i]] != array[i])
                        return false;
                }
                else
                {
                    if (hs.Add(array[i]))
                        dic.Add(pattern[i], array[i]);
                    else
                        return false;
                }
            }
            return true;
        }
}

public class Solution {
    public bool WordPattern(string pattern, string s) {
        var strs=s.Split(' ');
        if(strs.Length!=pattern.Length)
            return false;
        
        var dic=new Dictionary<char, string>();
        for(int i=0; i<pattern.Length; i++){
            var ch=pattern[i];
            if(dic.ContainsKey(ch)){
                if(dic[ch]!=strs[i])
                    return false;
            }
            else{
                if(dic.ContainsValue(strs[i]))
                    return false;
            }
            dic[ch]=strs[i];
        }
        return true;
    }
}
​

*/