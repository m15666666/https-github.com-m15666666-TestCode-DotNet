using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定两个字符串 s1 和 s2，写一个函数来判断 s2 是否包含 s1 的排列。

换句话说，第一个字符串的排列之一是第二个字符串的子串。

示例1:

输入: s1 = "ab" s2 = "eidbaooo"
输出: True
解释: s2 包含 s1 的排列之一 ("ba").
 

示例2:

输入: s1= "ab" s2 = "eidboaoo"
输出: False
 

注意：

输入的字符串只包含小写字母
两个字符串的长度都在 [1, 10,000] 之间 
*/
/// <summary>
/// https://leetcode-cn.com/problems/permutation-in-string/
/// 567. 字符串的排列
/// https://blog.csdn.net/qq_36059306/article/details/86470000
/// 
/// https://leetcode-cn.com/submissions/detail/22083529/
/// </summary>
class PermutationInStringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool CheckInclusion(string s1, string s2)
    {
        if (s1 == null || s1.Length == 0) return true;
        if (s2 == null || s2.Length == 0 || s2.Length < s1.Length) return false;
        if (s1.Length == 1 && s2.Length == 1) return s1[0] == s2[0];

        int s1Len = s1.Length;
        Dictionary<char, int> s1Map = new Dictionary<char, int>();
        foreach (var c in s1)
            if (!s1Map.ContainsKey(c)) s1Map.Add(c, 1);
            else ++s1Map[c];

        int left = 0;
        Dictionary<char, int> windowMap = new Dictionary<char, int>();
        var keys = s1Map.Keys;
        for ( int i = 0; i < s2.Length; i++)
        {
            var c = s2[i];

            if (!windowMap.ContainsKey(c)) windowMap.Add(c, 1);
            else ++windowMap[c];

            int windowLength = i - left + 1;
            if(windowLength < s1Len) continue;

            // 处理等于的情况 windowLength == s1Len
            bool allMatch = true;
            foreach( var key in keys )
                if(!windowMap.ContainsKey(key) || windowMap[key] != s1Map[key])
                {
                    allMatch = false;
                    break;
                }

            if (allMatch) return true;

            --windowMap[s2[left]];
            ++left;
        }
        return false;
    }
}
/*
public class Solution {
    public bool CheckInclusion(string s1, string s2) {
        if (s1.Length > s2.Length) return false;
        Dictionary<char, int> dict = new Dictionary<char, int>();
        int length = s1.Length;
        for (int i = 0; i < length; i++)
        {
            if (dict.ContainsKey(s1[i]))
            {
                dict[s1[i]]++;
            }
            else
            {
                dict.Add(s1[i], 1);
            }
        }

        for (int i = 0; i < length; i++)
        {
            if (dict.ContainsKey(s2[i]))
            {
                dict[s2[i]]--;
                if (dict[s2[i]] == 0) dict.Remove(s2[i]);
            }
            else
            {
                dict.Add(s2[i], -1);
            }
        }
        if (dict.Count == 0) return true;
        for (int i = length; i < s2.Length; i++)
        {
            int start = i - length;

            if (dict.ContainsKey(s2[start]))
            {
                dict[s2[start]]++;
                if (dict[s2[start]] == 0) dict.Remove(s2[start]);
            }
            else
            {
                dict.Add(s2[start], 1);
            }

            if (dict.ContainsKey(s2[i]))
            {
                dict[s2[i]]--;
                if (dict[s2[i]] == 0) dict.Remove(s2[i]);
            }
            else
            {
                dict.Add(s2[i], -1);
            }
            if (dict.Count == 0) return true;
        }
        return false;
    }
}
public class Solution {
    public bool CheckInclusion(string s1, string s2) {
        int n1 = s1.Length;
        int n2 = s2.Length;
        if (n1 > n2) {
            return false;
        }
        
        int [] count1 = new int[26];
        int [] count2 = new int[26];
        
        for (int i = 0; i < n1; i ++) {
            count1[(int)(s1[i]-'a')] ++;
            count2[(int)(s2[i]-'a')] ++;
        }
        
        if (EqualArr(count1, count2)) {
            return true;
        }
        
        for (int i = 1; i + n1 - 1 < n2; i ++) {
            // Console.Write("" + s2[i-1] + " " + s2[i+n1]);
            count2[(int)(s2[i-1]-'a')] --;
            count2[(int)(s2[i+n1 - 1]-'a')] ++;
            if (EqualArr(count1, count2)) {
                return true;
            }
        }
        return false;
    }
    
    bool EqualArr(int[] arr1, int[] arr2) {
        if (arr1.Length != arr2.Length)
            return false;
        
        for(int i = 0; i < arr1.Length; i ++) {
            if (arr1[i] != arr2[i]) {
                return false;
            }
        }
        return true;
    }
}
public class Solution {
    public bool CheckInclusion(string s1, string s2)  {
        
        if(s1.Length>s2.Length)return false;
        
        // init dict and count
        var dict=new Dictionary<char,int>();
        for(char i='a';i<='z';i++)dict[i]=0;
        
        foreach(var c in s1){
            dict[c]++;
        }
        
        for(int i=0;i<s1.Length;i++){
            dict[s2[i]]--;
        }
        if(Valid(dict))return true;
        
        for(int i=1;i<s2.Length-s1.Length+1;i++){
            dict[s2[i-1]]++;
            dict[s2[i+s1.Length-1]]--;
            if(Valid(dict))return true;
        }
        return false;
    }
    
    bool Valid(Dictionary<char,int> dict){
        foreach(var kv in dict)if(kv.Value!=0)return false;
        return true;
    }
}
*/
