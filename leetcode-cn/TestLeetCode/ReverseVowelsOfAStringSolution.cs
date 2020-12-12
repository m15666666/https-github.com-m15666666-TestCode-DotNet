/*
编写一个函数，以字符串作为输入，反转该字符串中的元音字母。

 

示例 1：

输入："hello"
输出："holle"
示例 2：

输入："leetcode"
输出："leotcede"
 

提示：

元音字母不包含字母 "y" 。

*/

using System.Collections.Generic;

/// <summary>
/// https://leetcode-cn.com/problems/reverse-vowels-of-a-string/
/// 345. 反转字符串中的元音字母
///
///
///
/// </summary>
internal class ReverseVowelsOfAStringSolution
{
    public void Test()
    {
        //int[] nums = new int[] {3, 2, 4};
        //int k = 6;
        //var ret = LevelOrder((int[]) nums.Clone(), k);

        //Console.WriteLine(string.Join(",", ret.Select(v => v.ToString())));
    }

    public string ReverseVowels(string s)
    {
        bool changed = false;
        char[] chars = s.ToCharArray();
        int left = 0, right = s.Length - 1;
        HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
        while (true)
        {
            while (left < right && !vowels.Contains(chars[left])) left++;
            while (left < right && !vowels.Contains(chars[right])) right--;
            if (right <= left) break;
            changed = true;
            (chars[left], chars[right]) = (chars[right], chars[left]);
            left++;
            right--;
        }
        return changed ? new string(chars) : s;
    }
}

/*

*/