using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*
给定一个字符串，验证它是否是回文串，只考虑字母和数字字符，可以忽略字母的大小写。

说明：本题中，我们将空字符串定义为有效的回文串。

示例 1:

输入: "A man, a plan, a canal: Panama"
输出: true
示例 2:

输入: "race a car"
输出: false


*/
/// <summary>
/// https://leetcode-cn.com/problems/valid-palindrome/
/// 125. 验证回文串
/// 
/// 
/// 
/// 
/// </summary>
class ValidPalindromeSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public bool IsPalindrome(string s)
    {
        int left = 0, right = s.Length - 1;
        while (left < right) 
        {
            while (left < right && !char.IsLetterOrDigit(s[left]))  ++left; 
            while (left < right && !char.IsLetterOrDigit(s[right])) --right; 
            if (left < right) 
            {
                if (char.ToLower(s[left]) != char.ToLower(s[right])) return false;
                ++left;
                --right;
            }
        }
        return true;
    }
}
/*

*/
