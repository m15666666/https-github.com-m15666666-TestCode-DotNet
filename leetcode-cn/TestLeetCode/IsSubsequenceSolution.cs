using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定字符串 s 和 t ，判断 s 是否为 t 的子序列。

你可以认为 s 和 t 中仅包含英文小写字母。字符串 t 可能会很长（长度 ~= 500,000），而 s 是个短字符串（长度 <=100）。

字符串的一个子序列是原始字符串删除一些（也可以不删除）字符而不改变剩余字符相对位置形成的新字符串。（例如，"ace"是"abcde"的一个子序列，而"aec"不是）。

示例 1:
s = "abc", t = "ahbgdc"

返回 true.

示例 2:
s = "axc", t = "ahbgdc"

返回 false.

后续挑战 :

如果有大量输入的 S，称作S1, S2, ... , Sk 其中 k >= 10亿，你需要依次检查它们是否为 T 的子序列。在这种情况下，你会怎样改变代码？ 
*/
/// <summary>
/// https://leetcode-cn.com/problems/is-subsequence/
/// 392. 判断子序列
/// </summary>
class IsSubsequenceSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsSubsequence(string s, string t)
    {
        if (string.IsNullOrWhiteSpace(s)) return true;
        if (string.IsNullOrWhiteSpace(t) || t.Length < s.Length) return false;

        int sIndex = 0;

        var sChar = s[sIndex++];
        foreach( var c in t )
        {
            if (c != sChar) continue;

            if (sIndex < s.Length) sChar = s[sIndex++];
            else return true;
        }
        return false;
    }
}
/*
public class Solution {
    public bool IsSubsequence(string s, string t) {
        int p = 0;
        int q = 0;
        
        for (p = 0; p < s.Length; p ++) {
            bool found = false;
            for (int j = q; j < t.Length; j ++, q ++) {
                if (t[q] == s[p]) {
                    found = true;
                    q ++;
                    break;
                }
            }
            
            if (found == false) {
                return false;
            }
        }
        
        return true;
        
    }
}
public class Solution {
    public bool IsSubsequence(string s, string t) {
        int si=0;
        int ti=0;
        while(si!=s.Length&&ti!=t.Length)
        {
            if(s[si]==t[ti]){si++;ti++;}
            else{ti++;}
        }
        if(si==s.Length)return true;
        else return false;
    }
}
public class Solution {
    public bool IsSubsequence(string s, string t) {
        int i1=0;
        int i2=0;
        while(i1<s.Length&&i2<t.Length){
            if(s[i1]==t[i2]){
                i1++;
                i2++;
            }else{
                i2++;
            }
        }
        return i1==s.Length;
    }
}
public class Solution {
    public bool IsSubsequence(string s, string t) {
        int index =0 ; 
        for(int i = 0 ; i < t.Length && index<s.Length ; i++){
            if(s[index] == t[i]){
                index++;
            }
        }
        if(index == s.Length){
            return true;
        }else
        {
            return false;
        }
    }
}

    using System.Text.RegularExpressions;
public class Solution {
    public bool IsSubsequence(string s, string t) {
        foreach(char c in s)
            {
                int index = 0;
                index = t.IndexOf(c);
                if (index < 0)
                    return false;
                else
                    t = t.Substring(index+1, t.Length - (index+1));
            }
            return true;
    }
}
public class Solution {
    public bool IsSubsequence(string s, string t) {
        if(s == null || s.Length == 0)
        {
            return true;
        }
        if(t == null || t.Length == 0)
        {
            return false;
        }        
        int i = 0,tmp = 0;
        for(i = 0;i < t.Length;i ++)
        {
            if(t[i] == s[tmp])
            {
                tmp ++;
                if(tmp == s.Length)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

public class Solution {
    public bool IsSubsequence(string s, string t)
    {
        if (s.Length == 0)
            return true;
        int SIndex = 0;
        for (int i = 0; i < t.Length; i++)
        {
            if (t[i] == s[SIndex])
                SIndex++;
            if (SIndex >= s.Length)
                return true;
        }
        return false;
    }
}
*/
