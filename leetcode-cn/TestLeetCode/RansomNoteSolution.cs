using System;
using System.Collections.Generic;

/*
给定一个赎金信 (ransom) 字符串和一个杂志(magazine)字符串，判断第一个字符串 ransom 能不能由第二个字符串 magazines 里面的字符构成。如果可以构成，返回 true ；否则返回 false。

(题目说明：为了不暴露赎金信字迹，要从杂志上搜索各个需要的字母，组成单词来表达意思。杂志字符串中的每个字符只能在赎金信字符串中使用一次。)

 

注意：

你可以假设两个字符串均只含有小写字母。

canConstruct("a", "b") -> false
canConstruct("aa", "ab") -> false
canConstruct("aa", "aab") -> true


*/

/// <summary>
/// https://leetcode-cn.com/problems/ransom-note/
/// 383. 赎金信
/// 
/// 
///
///
/// </summary>
internal class RansomNoteSolution
{
    public bool CanConstruct(string ransomNote, string magazine) {
        if (string.IsNullOrEmpty(ransomNote)) return true;
        if (magazine == null || magazine.Length < ransomNote.Length) return false;
        int[] charCount = new int[26];
        int count = 0;
        const char a = 'a';
        foreach (char c in ransomNote)
        {
            count++;
            charCount[c - a]++;
        }
        foreach(char c in magazine)
        {
            int index = c - a;
            if(0 < charCount[index])
            {
                --charCount[index];
                if (--count == 0) return true; ;
            }
        }
        return false;

    }

}

/*
public class Solution {
    public bool CanConstruct(string ransomNote, string magazine) {
                    if(string.IsNullOrEmpty(ransomNote))
            {
                return true;
            }   
        if (string.IsNullOrEmpty(magazine))
            {
                return false;
            }
            int[] magazineStat = new int[26];

            for (int i = 0; i < magazine.Length; i++)
            {
                magazineStat[magazine[i] - 'a']++;
            }

            for (int i = 0; i < ransomNote.Length; i++)
            {
                magazineStat[ransomNote[i] - 'a']--;
                if (magazineStat[ransomNote[i] - 'a'] < 0)
                {
                    return false;
                }
            }

            return true;        
    }
}



*/